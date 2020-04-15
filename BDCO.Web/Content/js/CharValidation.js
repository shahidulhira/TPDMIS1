function minmax(value, min, max) {
    if (parseInt(value) < min || isNaN(parseInt(value)))
        return min;
    else if (parseInt(value) > max)
        return max;
    else return value;
}

function onlyAlphabets(e, t) {
    try {
        if (window.event) { var charCode = window.event.keyCode; }
        else if (e) { var charCode = e.which; }
        else { return true; }

        if ((charCode > 64 && charCode < 91) || (charCode > 31 && charCode < 33) || (charCode > 96 && charCode < 123) || (charCode > 188 && charCode < 190)) return true;
        else return false;
    }
    catch (err) { alert(err.Description); }
}

function onlyNumber(e, t) {
    try {
        if (window.event) { var charCode = window.event.keyCode; }
        else if (e) { var charCode = e.which; }
        else { return true; }

        if ((charCode > 47 && charCode < 58)) return true;
        else return false;
    }
    catch (err) { alert(err.Description); }
}

function onlyDecimal(evt, element) {
    try {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8))
            return false;
        else {
            var len = $(element).val().length;
            var index = $(element).val().indexOf('.');
            if (index > 0 && charCode == 46) {
                return false;
            }
            
            //if (index > 0) {
            //    var CharAfterdot = (len + 1) - index;
            //    if (CharAfterdot > 3) {
            //        return false;
            //    }
            //}

        }
        return true ;
    }
    catch (err) { alert(err.Description); }
}