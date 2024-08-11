const dropdown = document.querySelector(".dropdown");
const account = document.querySelector(".account");

account.addEventListener
(
    "click",
    (event) =>
    {
        dropdown.classList.toggle("hide");
        event.stopPropagation();
    }
);

window.addEventListener
(
    "click",
    () =>
    {
        dropdown.classList.add("hide");
    }
);


const ul = document.querySelector(".navbar-main-ul");
const burger_menu = document.querySelector(".burger-menu");

burger_menu.addEventListener
(
    "click",
    (event) =>
    {
        ul.classList.toggle("hide-burger-menu");
        event.stopPropagation();
    }
);

window.addEventListener
(
    "click",
    () =>
    {
        ul.classList.add("hide-burger-menu");
    }
);