// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const steps = Array.from(document.querySelectorAll("form .step"));
const nextBtn = document.querySelectorAll("form .next-btn");
const prevBtn = document.querySelectorAll("form .previous-btn");
const form = document.querySelector("form");
nextBtn.forEach((button) => {
    button.addEventListener("click", () => {
        changeStep("next");
    });
});
prevBtn.forEach((button) => {
    button.addEventListener("click", () => {
        changeStep("prev");
    });
});

function changeStep(btn) {
    let index = 0;
    const active = document.querySelector(".active");
    index = steps.indexOf(active);
    steps[index].classList.remove("active");
  
    if (btn === "next") {
        index++;
    } else if (btn === "prev") {
        index--;
    }
    steps[index].classList.add("active");
}


/*==============Detail Page============================*/


function myFunction() {
    document.getElementById("Omschrijving-btn").addEventListener("click", ()=> {
        document.getElementById("Omschrijving").style.display = "inline-block";
        document.getElementById("Opmerkingen").style.display = "none";
        console.log("omschrijving btn")

    });
    document.getElementById("Opmerkingen-btn").addEventListener("click", () => {
        document.getElementById("Opmerkingen").style.display = "inline-block";
        document.getElementById("Omschrijving").style.display = "none";
        console.log("opmerking btn")
    });

}