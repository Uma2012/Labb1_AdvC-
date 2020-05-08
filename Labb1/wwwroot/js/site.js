// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function pageLoad() {
    alert('You Clicked Button');
}

function test() {
    document.getElementById('textfield').innerHTML='text changed'
}

function DisplayResponseMessage(productName) {

    // If productName is null, stop here! Dont show any message
    if (productName == null)
        return;
   

    document.getElementById('item_added_to_cart').innerHTML = productName;
    $('#exampleModalCenter').modal('show');
}



function addtocart(productid) {
      console.log(productId);

    //fetch("https://localhost:44333/ShoppingCart/AddToCart?productid=" + productid)  {
    //    method: "POST"
    //};
   
        //.then((response) => {

        //    // If response was ok, update cart and display a message with the product name telling it was added to cart.
        //    if (response.ok) {
        //        UpdateCartButton(); 
        //        DisplayResponseMessage(name);
        //        IncreaseSingleProductInCart(productId);
        //    } else {
        //        alert("Skit också, något gick fel. Försök igen eller kontakta vår sketna support!");
        //    }
        //});

}