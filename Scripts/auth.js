// Global Auth Verification Function
function verifyUserAccess(options = {}) {

    const pathParts = window.location.pathname.split("/");
    const id = pathParts[pathParts.length - 1];

    const hashKey = localStorage.getItem("HashKey");
    const userID = localStorage.getItem("UserID");

    // If no login → hide content immediately
    if (!userID) {
        if (options.hideSelector) {
            $(options.hideSelector).hide();
        }
        return;
    }

    $.ajax({
        url: options.verifyUrl,
        type: "POST",
        data: {
            UrlId: id,
            HashKey: hashKey
        },
        success: function (response) {

            if (!response || !response.isAuthorized) {

                // Clear storage
                localStorage.removeItem("UserID");
                localStorage.removeItem("Role");
                localStorage.removeItem("Username");
                localStorage.removeItem("HashKey");

                // UI changes
                $("#displayUsername").addClass("d-none");
                $("#loginBtn").removeClass("d-none");

                Swal.fire({
                    icon: "warning",
                    title: "Unauthorized Access!",
                    text: "You are not allowed to access this page!",
                    confirmButtonColor: "#dc3545"
                }).then(() => {
                    window.location.href = options.redirectUrl;
                });
            }
        },
        error: function () {
            console.error("Authorization check failed.");
        }
    });
}