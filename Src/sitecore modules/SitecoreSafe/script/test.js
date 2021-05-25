$("#idForm").submit(function (e) {

    e.preventDefault(); // avoid to execute the actual submit of the form.

    var form = $(this);
    
    $.ajax({
        type: "POST",
        url: "/api/sitecore/SitecoreSafe/TestForm",
        data: form.serialize(), // serializes the form's elements.
        success: function (data) {
            alert(data); // show response from the php script.
        }
    });


});