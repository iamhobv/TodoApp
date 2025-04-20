
function checkdarkMode() {

    let darkmode = localStorage.getItem('darkMode');
    if (darkmode == "on") {
        document.body.classList.add("dark-mode");

        console.log("on");

        const inputElements = document.querySelectorAll('input:not([type="submit"]):not([type="checkbox"])');
        inputElements.forEach(input => { input.classList.add('darkmode'); })

        const textarea = document.querySelectorAll('textarea');
        textarea.forEach(input => { input.classList.add('darkmode'); })



        const selectElements = document.querySelectorAll('select');
        selectElements.forEach(input => { input.classList.add('darkmode'); })


        const listGroup = document.querySelectorAll('.list-group-item');
        listGroup.forEach(input => { input.classList.add('darkmode'); })



        const muteGroup = document.querySelectorAll('.text-muted');
        muteGroup.forEach(input => { input.classList.add('text-unmuted'); })


        const chatGroup = document.querySelectorAll('.chat-container');
        chatGroup.forEach(input => { input.classList.add('chat-containerDark'); })


        const recMsgs = document.querySelectorAll('.received-message');
        recMsgs.forEach(input => { input.classList.add('received-messageDark'); })


        const sentMsgs = document.querySelectorAll('.sent-message');
        sentMsgs.forEach(input => { input.classList.add('sendMsgDark'); })


        const navpic = document.querySelector('.changenavPic');
        navpic.style.backgroundImage = "url('/images/bg-desktop-light.jpg')";




        const head = document.getElementById("userNameHead");

        if (head != null) {

            head.classList.add('UserNameHeader');
        }
        console.log(head);





    } else {

        const sentMsgs = document.querySelectorAll('.sent-message');
        sentMsgs.forEach(input => { input.classList.remove('sendMsgDark'); })
        const navpic = document.querySelector('.changenavPic');
        navpic.style.backgroundImage = "url('/images/bg-desktop-dark.jpg')";

        const recMsgs = document.querySelectorAll('.received-message');
        recMsgs.forEach(input => { input.classList.remove('received-messageDark'); })
        const muteGroup = document.querySelectorAll('.text-muted');
        muteGroup.forEach(input => { input.classList.remove('text-unmuted'); })
        const inputElements = document.querySelectorAll('input:not([type="submit"]):not([type="checkbox"])');
        inputElements.forEach(input => { input.classList.remove('darkmode'); })
        const selectElements = document.querySelectorAll('select');
        selectElements.forEach(input => { input.classList.remove('darkmode'); })
        console.log("off");
        document.body.classList.remove("darkmode");
        const listGroup = document.querySelectorAll('.list-group-item');
        listGroup.forEach(input => { input.classList.remove('darkmode'); })


        const textarea = document.querySelectorAll('textarea');
        textarea.forEach(input => { input.classList.remove('darkmode'); })

        const chatGroup = document.querySelectorAll('.chat-container');
        chatGroup.forEach(input => { input.classList.remove('chat-containerDark'); })

        const head = document.getElementById("userNameHead");

        if (head != null) {

            head.classList.remove('UserNameHeader');
        }
        console.log(head);


    }
}

checkdarkMode();

const ToggleButton = document.querySelector(".SwitchMood");
ToggleButton.addEventListener("click", () => {
    if (document.body.classList.contains("dark-mode")) {

        localStorage.setItem('darkMode', 'off');
        checkdarkMode();
        document.body.classList.remove("dark-mode");

    } else {
        localStorage.setItem('darkMode', 'on');
        checkdarkMode();
        document.body.classList.add("dark-mode");

    }

});

