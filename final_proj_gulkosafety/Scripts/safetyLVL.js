

function alertConstractor(content, alert_type_num, user_email, proj_num) {
    new_alert = {

        Content: content,
        Date: Date.now(),
        Alert_type_num: alert_type_num,
        User_email: user_email,
        Status: 0,
        Proj_num: proj_num
    }
    ajaxCall("POST", "../api/alert", JSON.stringify(new_alert), updateReportSuccess(), getError);
}


function safetyLVL(curentReport, lastREPORTdefects, gradesARR, projTypeWEIGHT, report_num) {
    gradesARR.pop()
    const maxGradeForDefectType = 25
    //מספר הנקודות שיורדות כתוצאה מבדיקת כללים בדוח הנוכחי
    const point = 10
    const max9OR10Grade = 2
    const maxdefects = 10
    total_grade = 0
    numOf9OR10Grade = 0
    pointTOreduce = 0
    counterOfreturnDefects = 0
    pointsOnDefectGrade10 = 0
    safetyLvLGrade = 0
    reportGradeAlert = "";
    safetyLvlAlert = ""
    numOfReturnDefectsStr=""
    namesOfReutrnDefectsStr = ""
    namesOf10GradeDefectsStr = "";
    namesOfUnFixedDefectsStr = "";

    for (var i = 0; i < projTypeWEIGHT.length; i++) {
        let sum = 0

        for (var j = 0; j < curentReport.length; j++) {
            if (projTypeWEIGHT[i].Defect_type_num == curentReport[j].Defect_type_num) {
                sum += curentReport[j].Grade;
                if (curentReport[j].Grade >= 9) {
                    numOf9OR10Grade++;
                    
                }
                if (curentReport[j].Grade == 10) {
                    pointsOnDefectGrade10 = pointsOnDefectGrade10 + 5
                    namesOf10GradeDefectsStr += " -נמצא ליקוי חמור בציון 10" + curentReport[j].Defect_name
                }
                if (lastREPORTdefects != "") {
                    for (var f = 0; f < lastREPORTdefects.length; f++) {

                        if (lastREPORTdefects[f].Defect_num == curentReport[j].Defect_num) {
                            counterOfreturnDefects++
                            switch (curentReport[j].Grade) {
                                case 1 - 5:
                                    pointTOreduce += 1;
                                    break;
                                case 6 - 8:
                                    pointTOreduce += 3;
                                    break;
                                default:
                                    pointTOreduce += 5;
                            }
                            namesOfReutrnDefectsStr += "נמצא ליקוי אשר הופיע בדוח הקודם-" + curentReport[j].Defect_name
                        }
                        //אם ליקוי לא תוקן משבוע שעבר ועבר לו תאריך התיקון נוסיף 2 מקודות להורדת הציון הכללית 
                        if (lastREPORTdefects[f].Fix_status == 0 && lastREPORTdefects[f].Fix_date<Date.now()) {

                            pointTOreduce += 2
                            namesOfUnFixedDefectsStr += "נמצא ליקוי אשר לא תוקן מהביקור הקודם-" + curentReport[j].Defect_name
                        }
                    }
                   
                }

                
            }

        }
        if (sum > maxGradeForDefectType) {
            sum = 100
        }
        total = sum * projTypeWEIGHT[i].Weight
        total_grade += total
    }
    if (counterOfreturnDefects > 0)
        numOfReturnDefectsStr += "סך כל מספר הליקויים החוזרים משבוע שעבר-" + counterOfreturnDefects
    else numOfReturnDefectsStr += "לא נמצאו ליקויים שחזרו על עצמם מהביקור הקודם."

    total_grade = total_grade + pointsOnDefectGrade10

    if (numOf9OR10Grade > max9OR10Grade) {
        total_grade += point
    }
    if (curentReport.length > maxdefects) {
        total_grade += point
    }
    total_grade = 100 - total_grade;
   //עדכון הציון של ההמשוכלל של הדוח הספציפי
    putNewReportGrage(report_num, total_grade)

    alert(total_grade + "to" + report_num)
    alert(pointTOreduce)

   sumOfAllReports= gradesARR.reduce(function (a, b) {
        return a + b;
    }, 0);
    avarge = (sumOfAllReports + total_grade) / (gradesARR.length + 1)
    alert("the avarge is:" + avarge)
    //ציון הפרוייקט נקבע על ידי הממוצע, נחסיר ממנו את המיקויים שחזרו משבוע שעבר
    safetyLvLGrade = avarge - pointTOreduce

    if ((total_grade - gradesARR[gradesARR.length-1])>=10) {
        safetyLvLGrade = safetyLvLGrade + 5;
    }

    let alretHtml = "<div id='safetyLevelAlert'>";
    alretHtml += "<p>ציון דוח ביקור:" + total_grade + "</p>";
    alretHtml += "<p>ציון רמת בטיחות פרויקט:" + safetyLvLGrade + "</p>";
    alretHtml += "<p>פירוט הדוח:</p>";
    alretHtml += "<p>" + namesOf10GradeDefectsStr + "</p>";
    alretHtml += "<p>" + numOfReturnDefectsStr + "</p>";
    alretHtml += "<p>" + namesOfUnFixedDefectsStr + "</p>";
    alretHtml += "</div>";
    $("#safetyAlert").html(alretHtml);
    //alertConstractor(alretHtml, 1, "gulkosafety@gmail.com", project.Proj_num);
    
}



function putNewReportGrage(reportNum,grade) {
   let api = "../api/report?report_num=" + reportNum + "&grade=" + grade
    ajaxCall("PUT", api, "", updateReportSuccess, getError);
}

function updateReportSuccess() {
alert("new grade success")
}
function getError(err) {
alert(err)
}
