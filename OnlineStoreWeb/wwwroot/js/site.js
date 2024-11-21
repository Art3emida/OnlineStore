function BindPaginationButtonMore({ buttonSelector, containerSelector, paginationSelector, total, limit, currentPage, url = window.location.href, method = 'POST' })
{
    let button = $(buttonSelector),
        offset = currentPage * limit;

    button.on('click', function () {
        $.ajax({
            url: url,
            type: method,
            data: { limit: limit, offset: offset },
            success: function (data) {
                $(containerSelector).append(data);
                offset += limit;
                currentPage++;
                let pagination = $(paginationSelector);

                if (pagination.length > 0)
                {
                    pagination.find('[data-pagination-page="' + currentPage + '"]:not(.active)').addClass('active');
                    let nextPageEl = pagination.find('#next-page');

                    if (nextPageEl.length > 0)
                    {
                        let nextPageUrl = nextPageEl.attr('href');
                        nextPageEl.attr(
                            'href',
                            nextPageUrl.replace(/page=\d+/, `page=${currentPage + 1}`)
                        )

                        if (total <= offset)
                            nextPageEl.remove();
                    }
                }

                if (offset >= total)
                    button.hide();
            }
        });
    });
}

function BindDropZone({ targetSelector, fieldName, filePrefix, existingFiles = [], maxFiles = 10, maxFilesize = 10, formSelector = '#mainForm', withRemoveButtons = true })
{
    new Dropzone(targetSelector, {
        url: '/file/upload',
        paramName: 'file',
        maxFiles: maxFiles,
        maxFilesize: maxFilesize,
        acceptedFiles: 'image/*',
        addRemoveLinks: withRemoveButtons,
        init: function () {
            this.on('addedfile', function (newFile) {
                if (maxFiles === 1 && existingFiles.length > 0) {
                    this.files.forEach((file) => {
                        this.removeFile(file);
                    });
                    existingFiles = [];
                }
            });
            
            this.on('sending', function (file, xhr, formData) {
                formData.append('filePrefix', filePrefix);
            });

            this.on('success', function (file, response) {
                file.serverFileName = response.fileName;
                $('<input>').attr({
                    type: 'hidden',
                    name:  fieldName + '[]',
                    value: file.serverFileName
                }).appendTo(formSelector);
            });

            this.on('removedfile', function (file) {
                if (!file.serverFileName)
                    return;

                $.ajax({
                    url: '/file/delete',
                    method: 'POST',
                    data: { fileName: file.serverFileName },
                    success: function () {
                        $(`input[name="${fieldName}[]"][value="${file.serverFileName}"]`).remove();
                    },
                    error: function () {
                        console.error('Error deleting file');
                    }
                });
            });

            this.on("maxfilesexceeded", function(file) {
                this.removeFile(file);
            });

            if (existingFiles.length > 0)
            {
                existingFiles.forEach(function(fileUrl) {
                    fileUrl = '/' + fileUrl;
                    let fileName = fileUrl.split('/').pop();
                    let existFile = {
                        name: fileName,
                        size: 0,
                        url: fileUrl,
                        serverFileName: fileName,
                        isExisting: true
                    };

                    this.emit("addedfile", existFile);
                    this.emit("thumbnail", existFile, fileUrl);
                    this.emit("complete", existFile);

                    const removeButton = existFile.previewElement.querySelector('.dz-remove');
                    if (removeButton)
                        removeButton.remove();
                }.bind(this));
            }
        }
    });
}

function AddToCart({productId, quantity, quantitySelector, redirect})
{
    if (quantitySelector)
        quantity = $(quantitySelector).length > 0 ? $(quantitySelector).val() : quantity;

    $.ajax({
        url: '/cart/add',
        type: 'POST',
        data: {
            productId: productId,
            quantity: quantity
        },
        success: function (response) {
            const cartItemCounter = $('#cartItemCounter');
            if (cartItemCounter.length)
            {
                const newCount = parseInt(cartItemCounter.text()) + 1;
                cartItemCounter.text(newCount);
                cartItemCounter.removeClass('bg-secondary');
                cartItemCounter.addClass('bg-success');
            }

            if (redirect)
                window.location.href = '/cart';
        },
        error: function (xhr, status, error) {}
    });
}

let cartItemQuantityChangeTimeout;
function СartItemQuantityChanged(productId, quantity)
{
    clearTimeout(cartItemQuantityChangeTimeout);
    cartItemQuantityChangeTimeout = setTimeout(function () {
        UpdateСartItemQuantity(productId, quantity);
    }, 1000);
}

function UpdateСartItemQuantity(productId, quantity)
{
    $.ajax({
        url: '/cart/update',
        type: 'POST',
        data: { productId: productId, quantity: quantity },
        success: function (response) {
            $('#cart-container').html(response);
        },
        error: function () {}
    });
}

function RemoveСartItem(productId)
{
    $.ajax({
        url: '/cart/remove',
        type: 'POST',
        data: { productId: productId },
        success: function (response) {
            $('#cart-container').html(response);

            const cartItemCounter = $('#cartItemCounter');
            if (cartItemCounter.length)
            {
                const newCount = parseInt(cartItemCounter.text()) - 1;
                cartItemCounter.text(newCount);
            }
        },
        error: function () {}
    });
}
