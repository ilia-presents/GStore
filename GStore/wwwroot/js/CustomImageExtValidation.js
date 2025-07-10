jQuery.validator.addMethod("imageExtValidation",
    function (value, element, params) {

        if (value) {

            let extension = value.split('.').pop();

            extension = extension.toLowerCase();

            const fileExtensions = ["jpeg", "jpg"];

            let boolTemp = fileExtensions.includes(extension);

            return boolTemp;
        }

        return false;
    });

jQuery.validator.unobtrusive.adapters.addBool("imageExtValidation");