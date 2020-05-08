// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function DisplayResponseMessage(productName) {
    console.log(productName);
    // If productName is null, stop here! Dont show any message
    if (productName == null)
        alert("Something went wrong!");

    document.getElementById('item_added_to_cart').innerHTML = productName;
    $('#exampleModalCenter').modal('show');
}



function addtocart(productid, name) {
    console.log(name, productid);
    
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
                DisplayResponseMessage(name);
            }
            else {
                alert("Something went wrong!");
            }

        });
   
  
}

function GetAntiForgerytoken() {
    return document.getElementById('AntiForgeryToken').innerHTML;
}