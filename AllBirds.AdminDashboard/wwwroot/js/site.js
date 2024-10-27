// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function closeMsg(evt) {
    let parent = evt.target.children.length ? evt.target.parentElement : evt.target.parentElement.parentElement;
    console.log(parent);
    parent.setAttribute("style", "display:none;");
}