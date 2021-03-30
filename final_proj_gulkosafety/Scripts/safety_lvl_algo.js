function alertConstractor(arr, content, alert_type_num, user_email,proj_num) {
    new_alert = {
       
        Content: content,
        Date: Date.now(),
        Alert_type_num: alert_type_num,
        User_email: user_email,
        Status: 0,
        Proj_num: proj_num
    }
    arr.push(new_alert)
}


function safetyLVL(curentReport, lastREPORTdefects, gradesARR, projTypeWEIGHT) {
    const maxGradeForDefectType = 25
    //מספר הנקודות שיורדות כתוצאה מבדיקת כללים בדוח הנוכחי
    const point = 10
    const max9OR10Grade = 2
    const   maxdefects=10
    total_grade = 0
    numOf9OR10Grade = 0
    pointTOreduce = 0
    counterOfreturnDefects=0
    alert_arr = [];
    pointsOnDefectGrade10=0
    for (var i = 0; i < projTypeWEIGHT.length; i++) {
        let sum = 0
        
        for (var j = 0; j < curentReport.length; j++) {
            if (projTypeWEIGHT[i].Defect_type_num == curentReport[j].Defect_type_num) {
                sum += curentReport[j].Grade;
                if (curentReport[j].Grade>=9) {
                    numOf9OR10Grade++;
                    
                }
                if (curentReport[j].Grade==10) {
                    pointsOnDefectGrade10 = pointsOnDefectGrade10+5
                }
                if (lastREPORTdefects!="") {
                    for (var f = 0; f < lastREPORTdefects.length; f++) {
                        
                        if (lastREPORTdefects[f].Defect_num == curentReport[j].Defect_num) {
                            counterOfreturnDefects++
                            switch (curentReport[j].Grade) {
                                case 1 - 5:
                                    pointTOreduce = 1;
                                    break;
                                case 6 - 8:
                                    pointTOreduce = 3;
                                    break;
                                default:
                                    pointTOreduce = 5;
                            }
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
 
    total_grade = total_grade + pointsOnDefectGrade10
    
    if (numOf9OR10Grade > max9OR10Grade) {
        total_grade += point
    }
    if (curentReport.length > maxdefects) {
        total_grade += point
    }
    total_grade = 100 - total_grade;
    //put report-grade

    alert(total_grade)
    alert(pointTOreduce)
    //avg,הורדת ציונים בין דוחות וחישוב רמת בטיחות פרויקט
    //שילוב התראות בכל האלגוריתם
}