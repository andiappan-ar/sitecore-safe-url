$("#idForm").submit(function (e) {

    e.preventDefault(); // avoid to execute the actual submit of the form.

    grecaptcha.ready(function () {
        grecaptcha.execute('6LcG3e4aAAAAAB4LA6IsLetrSnrsX_9_YHkd3epL', { action: 'submit' }).then(function (token) {
            // Add your logic to submit to your backend server here.
            $("#recaptcah-response").val(token);
            var form = $("#idForm");           
            $.ajax({
                type: "POST",
                url: "/api/sitecore/SitecoreSafe/TestForm",
                data: form.serialize(), // serializes the form's elements.
                success: function (data) {
                    alert(data); // show response from the php script.
                }
            });
        });
    });



});