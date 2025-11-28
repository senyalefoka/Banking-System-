document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('personalDetailForm');
    const submitBtn = document.getElementById('submitBtn');
    const spinner = document.getElementById('loadingSpinner');
    const buttonText = document.getElementById('buttonText');

    if (form && submitBtn) {
        form.addEventListener('submit', function (e) {
            setTimeout(() => {
                if (form.checkValidity()) {
                    submitBtn.disabled = true;
                    spinner.classList.remove("d-none");
                    buttonText.textContent = "Adding...";
                }
            }, 100);
        });
    }
});