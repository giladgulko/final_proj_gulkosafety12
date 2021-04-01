
function safetyLeveL(curentReport, lastREPORTdefects, gradesARR, projTypeWEIGHT, report_num) {
    gradesARR.pop()
    const maxGradeForDefectType = 25
    //מספר הנקודות שיורדות כתוצאה מבדיקת כללים בדוח הנוכחי
    const point = 10
    const maxSevereDefects = 2
    const maxdefects = 10
    report_Grade = 0
    safetyLvLGrade = 0

    pointTOreduce = 0
    pointsOnDefectGrade10 = 0

    severeDefects = [];
    TenGradeDefects = [];
    returnDefects = [];
    notFixDefects = [];

    //calculate report grade
    for (var i = 0; i < projTypeWEIGHT.length; i++) {
        let sum = 0
        //runs over current report defects and sum the grades
        for (var j = 0; j < curentReport.length; j++) {
            if (projTypeWEIGHT[i].Defect_type_num == curentReport[j].Defect_type_num) {
                sum += curentReport[j].Grade;
               
                if (curentReport[j].Grade >= 9) { //count num of severe defects- grade 9/10
                    severeDefects.push(curentReport[j]);   
                }
               
                if (curentReport[j].Grade == 10) { //for every 10 grade defect- add points to reduce
                    pointsOnDefectGrade10 = pointsOnDefectGrade10 + 5
                    TenGradeDefects.push(curentReport[j]);   
                }
                
                if (lastREPORTdefects != "") {//checking return defects from last week
                    for (var f = 0; f < lastREPORTdefects.length; f++) {

                        if (lastREPORTdefects[f].Defect_num == curentReport[j].Defect_num) {
                            returnDefects.push(curentReport[j]);
                            
                            switch (curentReport[j].Grade) {//reduce points for return def by severe
                                case 1 - 5:
                                    pointTOreduce += 1;
                                    break;
                                case 6 - 8:
                                    pointTOreduce += 3;
                                    break;
                                default:
                                    pointTOreduce += 5;
                            }                            
                        }
                        //אם ליקוי לא תוקן משבוע שעבר ועבר לו תאריך התיקון נוסיף 2 מקודות להורדת הציון הכללית 
                        if (lastREPORTdefects[f].Fix_status == 0 && lastREPORTdefects[f].Fix_date<Date.now()) {
                            pointTOreduce += 2
                            notFixDefects.push(lastREPORTdefects[f]);
                        }
                    }
                   
                }

                
            }

        }
        //if sum of all defects in same category > 25, the category gets max weight
        if (sum > maxGradeForDefectType) {
            sum = 100
        }
        //calc total grade of defect type and add to report grade
        total = sum * projTypeWEIGHT[i].Weight
        report_Grade += total
    }


    report_Grade = report_Grade + pointsOnDefectGrade10

    if (severeDefects.length > maxSevereDefects) {//if there is over 2 severe def reduce points
        report_Grade += point
    }
    if (curentReport.length > maxdefects) {//if num of defects bigger than 10 reduce points
        report_Grade += point
    }
    report_Grade = 100 - report_Grade;//calc final report grade
 
    updateReportGrade(report_num, report_Grade);  //PUT report grade on DB


    //start calc safety level
   sumOfAllReports= gradesARR.reduce(function (a, b) {
        return a + b;
    }, 0);

    avarge = (sumOfAllReports + report_Grade) / (gradesARR.length + 1) //calc reports avg
   
    safetyLvLGrade = avarge - pointTOreduce //avg is base grade, reducing points from rules

    if ((report_Grade - gradesARR[gradesARR.length-1])>=10) { //if current report grade higher in 10 points than the last one- add points
        safetyLvLGrade = safetyLvLGrade + 5;
    }

    updateProjectSafetyLevel(safetyLvLGrade);

    let str = "";
    let alretHtml = "<h2>סיכום עדכון רמת בטיחות פרויקט " + project.Project_num + "</h2>";
    alretHtml += "<h3>ציון דוח ביקור מספר " + report_num + ": "+ report_Grade + "</h3>";
    alretHtml += "<h3>ציון רמת בטיחות פרויקט: " + safetyLvLGrade + "</h3><br />";
    alretHtml += "<h3>פירוט ממצאים דוח ביקור:</h3>";
    if (curentReport.length > maxdefects) {//if num of defects bigger than 10 reduce points
        alretHtml += "<span>מספר הליקויים בדוח גבוה מ-10</span>";
    }
    alretHtml += "<span>נמצאו " + severeDefects.length + " ליקויים חמורים בביקור הנוכחי:</span>";
    for (var d = 0; d < severeDefects.length; d++) {
        alretHtml += "<p>" + severeDefects[d].Defect_type_name + "- " + severeDefects[d].Defect_name+" </p>";
    }

    alretHtml += "<p>נמצאו " + severeDefects.length + " ליקויים חמורים ביותר בציון 10: ";
    for (var d1 = 0; d1 < TenGradeDefects.length; d1++) {
        alretHtml += "<p>" + TenGradeDefects[d1].Defect_name + " </p>";
    }
    alretHtml += "<h3>פירוט ממצאים רמת הבטיחות:</h3>";
    alretHtml += "<p>ממוצע ציוני דוחות הביקור בפרויקט: " + avarge + "</p >";
    if (returnDefects.length > 0) {
        alretHtml += "<p>מספר הליקויים החוזרים מהביקור הקודם: " + returnDefects.length + "</p>";
        for (var r = 0; r < returnDefects.length; r++) {
            alretHtml += "<p>" + returnDefects[r].Defect_type_name + "- " + returnDefects[r].Defect_name + " </p>";
        }     
    } else alretHtml += "<p>לא נמצאו ליקויים שחזרו על עצמם מהביקור הקודם</p>";

    if (notFixDefects > 0) {
        alretHtml += "<p>ליקויים מהביקור הקודם שלא תוקנו בזמן: </p>";
        for (var n = 0; n < notFixDefects.length; n++) {
            alretHtml += "<p>" + notFixDefects[n].Defect_name + " </p>";
        }
    }
        
    if ((report_Grade - gradesARR[gradesARR.length - 1]) >= 10) { 
        alretHtml += "<p>ניכר שיפור משמעותי: ציון הדוח הנוכחי גבוה ב-10 נק' מציון הדוח הקודם</p>";
    }
    $("#safetyAlert").html(alretHtml);
    $("#ReportGradeP").html(report_Grade);
    $("#safetyLvlGraph").html(safetyLvLGrade);
    alertConstractor(alretHtml, 1, "gulkosafety@gmail.com", project.Project_num);
    
}

function updateReportGrade(reportNum,grade) {
    let api = "../api/report?report_num=" + reportNum + "&grade=" + grade;
    ajaxCall("PUT", api, "", updateReportGradeSuccess, getError);
}

function updateReportGradeSuccess() {
    console.log("update report grade success");
}

function updateProjectSafetyLevel(safetyGrade) {
    let api = "../api/project?project_num=" + project.Project_num + "&safety_lvl=" + safetyGrade;
    ajaxCall("PUT", api, "", updateProjectSafetySuccess, getError);
}

function updateProjectSafetySuccess() {
    console.log("update project safety level success");
}

function getError(err) {
alert(err)
}

function alertConstractor(content, alert_type_num, user_email, proj_num) {
    new_alert = {
        Content: content,
        Date: Date.now(),
        Alert_type_num: alert_type_num,
        User_email: user_email,
        Status: 0,
        Proj_num : proj_num
    }
    ajaxCall("POST", "../api/alert", JSON.stringify(new_alert), insertAlertSuccess, getError);
}

function insertAlertSuccess() {
    alert("התראה הועלתה");
}