window.onload = function() {
    // Adiciona a imagem ao lado do título
    var titleElement = document.querySelector('.swagger-ui .topbar .topbar-wrapper .link');
    if (titleElement) {
        var img = document.createElement('img');
        img.src = '/swagger-custom/orion_big.png'; 
        img.style.height = '70px';
        img.style.marginRight = '30px';

        // Inserir a imagem antes do título
        titleElement.insertBefore(img, titleElement.firstChild);
    }
}