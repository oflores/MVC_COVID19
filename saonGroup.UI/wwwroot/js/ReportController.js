
$(document).ready(function () {
    var urlBase =  '@Url.Action("Province", "Report")?ISO=' ;
    console.log(urlBase);
    console.log(urlBase2);
    $("#btnReport").click(function () {
        let strISO = $("#btnIso").val();
       
        console.log( $("#btnIso").val());
        alert("CLICK"); 
        if (strISO !== "") { 
            $.get(urlBase  + strISO, function (data) {
                $('#detailsProv').html(data);
                $('#detailsProv').show();
                $('#tableRegion').hide();
            });
          
        } else {
            $('#detailsProv').hide();
            $('#tableRegion').show();
            
        }
      
    });
});