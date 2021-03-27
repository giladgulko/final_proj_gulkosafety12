

function safetyLVL(curentReport, lastREPORTdefects, gradesARR, projTypeWEIGHT) {
    const maxGradeForDefectType = 25
    //מספר הנקודות שיורדות כתוצאה מבדיקת כללים בדוח הנוכחי
    const point = 5
    const max9OR10Grade = 2
    const   maxdefects=10
    total_grade = 0
    numOf9OR10Grade = 0
    pointTOreduce=0
    for (var i = 0; i < projTypeWEIGHT.length; i++) {
        let sum = 0
        
        for (var j = 0; j < curentReport.length; j++) {
            if (projTypeWEIGHT[i].defect_type_num == curentReport[j].defect_type_num) {
                sum += curentReport[j].grade;
                if (curentReport[j].grade>=9) {
                    numOf9OR10Grade++;
                }
                if (lastREPORTdefects.include(curentReport[j])) {
                    switch (curentReport[j].grade) {
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
        if (sum > maxGradeForDefectType) {
            sum = 100
        }
        total = sum * projTypeWEIGHT[i].weight
        total_grade += total
    }
 

    
    if (numOf9OR10Grade > max9OR10Grade) {
        total_grade += point
    }
    if (curentReport.length > maxdefects) {
        total_grade += point
    }
    total_grade = 100 - total_grade;
    //put report-grade

    
   
}