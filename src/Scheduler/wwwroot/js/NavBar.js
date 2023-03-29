window.onscroll = function () { fixToTop() };

var navbar = document.getElementById("topNav");

var sticky = navbar.offsetTop;

function fixToTop() {
    if (window.pageYOffset > sticky) {
        navbar.classList.add("nav-fixed")
    } else {
        navbar.classList.remove("nav-fixed");
    }
}