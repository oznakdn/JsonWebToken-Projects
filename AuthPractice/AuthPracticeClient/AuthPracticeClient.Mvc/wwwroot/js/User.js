const url = "http://localhost:5173/api/user-hub";
const connection = new signalR.HunConnectionBuilder().withUrl(url).build();
connection.start();

const message = Document.getElementById("message");


connection.on("UserJoined", (value) => {
    message.text = value;
});