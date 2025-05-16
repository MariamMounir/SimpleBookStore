document.addEventListener('DOMContentLoaded', () => {

    // Add to cart
    document.querySelectorAll('.add-to-cart').forEach(button => {
        button.addEventListener('click', (e) => {
            const productId = button.dataset.productId;
            const formData = new FormData();
            formData.append('productId', productId);

            fetch('/Cart/AddToCart', {
                method: 'POST',
                body: formData
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        toastr.success(data.message || 'Product added to cart successfully');
                       
                    } else {
                        toastr.error(data.message || 'Failed to add product to cart');
                    }
                })
                .catch(() => toastr.error('Error adding product to cart'));
        });
    });
});
