document.addEventListener('DOMContentLoaded', () => {
    function updateTotals() {
        const rows = document.querySelectorAll('#cartBody tr');
        let subtotal = 0;

        rows.forEach(row => {
            const qtyInput = row.querySelector('.quantity');
            const price = parseFloat(row.querySelector('.unit-price').textContent.replace('$', ''));
            const qty = parseInt(qtyInput.value);
            const total = (qty * price).toFixed(2);
            row.querySelector('.item-total').textContent = '$' + total;

            subtotal += qty * price;

        });
        document.getElementById('cartSubtotal').textContent = '$' + subtotal.toFixed(2);
    }

    //updatequantity
    document.querySelectorAll('.quantity').forEach(input => {
        input.addEventListener('change', (e) => {
            const newQuantity = e.target.value;
            const cartItemId = e.target.dataset.cartItemId;

            const formData = new FormData();
            formData.append('cartItemId', cartItemId);
            formData.append('newQuantity', newQuantity);

            fetch('/Cart/UpdateQuantity', {
                method: 'POST',
                body: formData
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        toastr.success(data.message || 'Quantity updated successfully');
                        updateTotals();
                    } else {
                        toastr.error(data.message || 'Failed to update quantity');
                    }
                })
                .catch(() => toastr.error('Error updating quantity'));
        });
    });

    //delete item
    document.querySelectorAll('.remove-item').forEach(button => {
        button.addEventListener('click', (e) => {
            const cartItemId = e.target.closest('button').dataset.cartItemId;
            const formData = new FormData();
            formData.append('cartItemId', cartItemId);
            fetch('/Cart/RemoveFromCart', {
                method: 'POST',
                body: formData
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        e.target.closest('tr').remove();
                        toastr.success(data.message || 'Item removed successfully');
                        updateTotals();

                    } else {
                        toastr.error(data.message || 'Failed to delete item');
                    }
                })
                .catch(() => toastr.error('Error deleting item'));
        });
    });
    updateTotals();
});

