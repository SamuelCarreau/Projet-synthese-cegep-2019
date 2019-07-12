var deleteWorkshopType = function (id) {
    if (confirm("Voulez-vous vraiment supprimer ce volet?")) {
        $.ajax({
            url: "/api/WorkshopTypeApi/Delete/" + id,
            method: 'DELETE',
            success: function () {
                alert("Le volet à bien été désactivé");
                location.reload();
            },
            error: function (reponse) {
                if (reponse.responseJSON.errors != undefined) {
                    alert(reponse.responseJSON.errors[0].errorMessage);
                } else {
                    alert("Erreur lors de la suppression");
                }
            }
        });
    }
};