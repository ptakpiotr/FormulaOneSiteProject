"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:5001/comments").build();
var postId = window.location.href.substring(window.location.href.lastIndexOf("=")+1);

connection.start().then(() => {
});

connection.on(`NewComment${postId}`, (data) => {
    //dodawac do strony
    console.log(data);
});

function LikeThis(id) {
    connection.invoke("UpdateLikes", JSON.stringify({
        id: postId,
        commId: id
    }));
}

function AddComment() {
    connection.invoke("AddComment", JSON.stringify({
        postId: postId,
        content: document.getElementById("commentBox").value
    }));
}