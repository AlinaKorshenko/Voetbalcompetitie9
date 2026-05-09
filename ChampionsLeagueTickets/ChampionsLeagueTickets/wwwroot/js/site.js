document.addEventListener('DOMContentLoaded', function () {
    const accountIcon = document.querySelector('.account-icon');
    const dropdownMenu = document.querySelector('.account-dropdown .dropdown-menu');

    if (accountIcon && dropdownMenu) {
        accountIcon.addEventListener('click', function (e) {
            e.preventDefault();
            dropdownMenu.classList.toggle('show');
        });

        document.addEventListener('click', function (e) {
            if (!e.target.closest('.account-dropdown')) {
                dropdownMenu.classList.remove('show');
            }
        });
    }
});