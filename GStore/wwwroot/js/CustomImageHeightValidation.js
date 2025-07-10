jQuery.validator.addMethod("imageHeightValidation",
    function (value, element, params) {

        //const img = document.createElement('img');

        let files = $(element).prop("files");

        const file = files[0];



        /*if (selectedImage) { }*/

        //const img = new Image();

        //img.src = value;

        var img = new Image();

        

            let boolTemp;

            img.onload = () => {

                boolTemp = img.height > 1023;

                return boolTemp;
        }

        img.src = window.URL.createObjectURL(file)

    });

jQuery.validator.unobtrusive.adapters.addBool("imageHeightValidation");