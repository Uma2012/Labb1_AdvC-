// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




function addtocart(productid, name) {
    // console.log(name, productid);

    let formData = new FormData();

    // Append form data
    formData.append("productid", productid);
    formData.append("__RequestVerificationToken", GetAntiForgerytoken());
    fetch("https://localhost:44333/ShoppingCart/AddToCart", {
        method: "Post",
        body: formData

    })
        .then((response) => {
            if (response.ok) {
                updatecartamount();
                DisplayResponseMessage(name);
            }
            else {
                alert("Something went wrong!");
            }

        });

}

function DisplayResponseMessage(productName) {
    //console.log("productname: "productName);
    // If productName is null, stop here! Dont show any message
    if (productName == null)
        alert("Something went wrong!");

    document.getElementById('item_added_to_cart').innerHTML = productName;
    $('#exampleModalCenter').modal('show');
}

function updatecartamount() {
    console.log("Update cart button");
    let formData = new FormData();
    formData.append("__RequestVerificationToken", GetAntiForgerytoken());

    // Get cart content using an AJAX call to GetCartContent() actionmethod
    fetch('https://localhost:44333/ShoppingCart/UpdateCart', {
        method: "Post",
        body: formData
    })

        // Get response as Json data
        .then((response) => {
            return response.json();
        })

        // Update cart button with totalItems by manipulating DOM
        .then((data) => {
            console.log("data: ", data)
           document.getElementById('cart-amount').innerHTML = data;
           
        });
}




//$(document).ready(function () {
//    updatecartamount();
//})

function GetAntiForgerytoken() {
    return document.getElementById('AntiForgeryToken').innerHTML;
}

window.onload = (event) => { updatecartamount() };