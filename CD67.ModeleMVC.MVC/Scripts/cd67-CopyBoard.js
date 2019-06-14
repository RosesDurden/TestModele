// Class : copyboard
// le contenu de data-copyboard-content est copié dans le presse papier sur un click
$(document).on('click', ".copyboard", function () {
    const el = document.createElement('textarea');  // Create a <textarea> element
    el.value = $(this).data("copyboard-content");   // Set its value to the string that you want copied
    el.setAttribute('readonly', '');                // Make it readonly to be tamper-proof
    el.style.position = 'absolute';
    el.style.left = '-9999px';                      // Move outside the screen to make it invisible
    this.parentNode.appendChild(el);                // Append the <textarea> element to the parent element
    el.select();                                    // Select the <textarea> content
    document.execCommand('copy');                   // Copy - only works as a result of a user action (e.g. click events)
    this.parentNode.removeChild(el);                // Remove the <textarea> element
});
