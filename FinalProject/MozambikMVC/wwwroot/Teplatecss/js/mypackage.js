$(document).ready(() => {

    $("#bag").click(function () {
        var data = JSON.parse(localStorage.getItem($("#userId").val()));
        AjaxOperation(data);

        $("#w3lssbmincart").show();
    });
    function AjaxOperation(data) {
        $.ajax({
            url: "/home/PartialView",
            type: "POST",
            data: { LSModel: data },
            success: function (res) {
                $("#w3lssbmincart").html("");
                $("#w3lssbmincart").append(res);
                $("#hideBag").click(() => {
                    $("#w3lssbmincart").hide();
                    $("#w3lssbmincart").html("");
                });
            }
        })
    }
    $(".clickedButton").click(function () {
        let obj = {
            id: $(this).siblings(".id").val(),
            quantity: 1,
            name: $(this).siblings(".name").val(),
            price: $(this).siblings(".price").val(),
            price_1: $(this).siblings(".price").val(),
        }
        var data = JSON.parse(localStorage.getItem($("#userId").val()));
        console.log(data);
        let isHere = false;
        let index = 0;
        if (data == null) {
            data = [];
        }
        for (let i in data) {
            if (data[i].id == obj.id) {
                isHere = true;
                index = i;
                break;
            }
        }
        if (isHere) {
            data[index].quantity += obj.quantity;
            data[index].price = data[index].quantity * obj.price;
        } else {
            data.push(obj);
        }
        var parsedData = JSON.stringify(data);
        AjaxOperation(data);
        localStorage.setItem($("#userId").val(), parsedData);

    })
   
})


