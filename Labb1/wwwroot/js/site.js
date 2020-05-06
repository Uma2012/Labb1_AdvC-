// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function DisplayResponseMessage(productName) {

    // If productName is null, stop here! Dont show any message
    if (productName == null)
        return;

    document.getElementById('item_added_to_cart').innerHTML = productName;
    $('#exampleModalCenter').modal('show');
}

