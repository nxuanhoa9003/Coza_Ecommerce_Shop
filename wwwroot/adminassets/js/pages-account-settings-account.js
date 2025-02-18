/**
 * Account Settings - Account
 */

'use strict';

document.addEventListener('DOMContentLoaded', function (e) {
    (function () {
        const deactivateAcc = document.querySelector('#formAccountDeactivation');

        // Update/reset user image of account page
        let accountUserImage = document.getElementById('uploadedAvatar');
        const fileInput = document.querySelector('.account-file-input'),
            resetFileInput = document.querySelector('.account-image-reset');

        if (accountUserImage) {
            const resetImage = accountUserImage.src;
            fileInput.onchange = () => {
                if (fileInput.files[0]) {
                    accountUserImage.src = window.URL.createObjectURL(fileInput.files[0]);
                }
            };
            resetFileInput.onclick = () => {
                fileInput.value = '';
                accountUserImage.src = resetImage;
            };
        }



        // Handle 'Chọn tất cả' checkbox
        const selectAllCheckbox = document.getElementById("choosee_all_claims");
        const claimCheckboxes = document.querySelectorAll(".input_checkbox_claims");

        selectAllCheckbox.addEventListener("change", function () {
            const isChecked = selectAllCheckbox.checked;
            claimCheckboxes.forEach(checkbox => {
                checkbox.checked = isChecked;
            });
        });

        claimCheckboxes.forEach(checkbox => {
            checkbox.addEventListener("change", updateSelectAllCheckbox);
        });

        function updateSelectAllCheckbox() {
            const allChecked = Array.from(claimCheckboxes).every(checkbox => checkbox.checked);
            selectAllCheckbox.checked = allChecked;
        }
        updateSelectAllCheckbox();


    })();


   
});






