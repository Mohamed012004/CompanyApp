// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let InputSearcch = document.getElementById("SearchInput");

InputSearcch.addEventListener("keyup", () =>
{
    let xhr = new XMLHttpRequest();
    let url = 'https://localhost:44378/Employee?SearchInput=${InputSearch.value} ';

    // Initialize the request
    xhr.open("GET", url, true);

    // Define the callback function
    xhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            console.log(JSON.parse(this.responseText));
        }
    };

    // Send the request
    xhr.send();
})
