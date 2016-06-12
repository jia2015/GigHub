
var FollowingService = function () {
    var follow = function (artistId, done, fail) {
        $.post("/api/followings", { followeeId: artistId })
            .done(done)
            .fail(fail);
    };

    var unfollow = function (artistId, done, fail) {
        $.ajax({
            url: "/api/followings/" + artistId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        follow: follow,
        unfollow: unfollow
    };
}();