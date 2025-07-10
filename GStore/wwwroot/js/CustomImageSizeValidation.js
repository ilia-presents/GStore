jQuery.validator.addMethod("imageSizeValidation",
    function (value, element, params) {

        let files = $(element).prop("files");

        let file = files[0];

        if (file) {
            let fileSize = file.size;

            let boolTemp = fileSize < 7 * 1024 * 1024;  /// params[1]  10000000 

            return boolTemp;
        }

        return false;

    });

jQuery.validator.unobtrusive.adapters.addBool("imageSizeValidation");