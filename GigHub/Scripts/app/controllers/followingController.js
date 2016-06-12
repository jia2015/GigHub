
var FollowingController = function (followingService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-following", toogleFollowing);
    };

    var toogleFollowing = function (e) {
        button = $(e.target);

        var artistId = button.attr("data-user-id");

        if (button.hasClass("btn-info")) {
            followingService.unfollow(artistId, done, fail);
        } else {
            followingService.follow(artistId, done, fail);
        }
    };

    var done = function () {
        var text = (button.text() === "Follow") ? "Following" : "Follow";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("you already followed or something failed!");
    };

    return {
        init: init
    };
}(FollowingService);