//Delete button
let deleteButton = document.querySelectorAll(".deleteButton");

deleteButton.forEach(btn => btn.addEventListener("click", function (e) {
    e.preventDefault();

    let url = btn.getAttribute("href")

    Swal.fire({
        title: 'Əminsiniz?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Bəli!',
        cancelButtonText: 'Xeyr!'
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(url)
                .then(res => {
                    if (res.status == 200) {
                        Swal.fire({
                            title: 'Uğurla silindi!',
                            icon: 'success',
                        }).then(answer => {
                            if (answer.isConfirmed) {
                                window.location.reload(true);
                            }
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Silinmədi!',
                            icon: 'error',
                        });
                    }
                })
        }
    })
}));


//Restore button
let restoreButton = document.querySelectorAll(".restoreButton");

restoreButton.forEach(btn => btn.addEventListener("click", function (e) {
    e.preventDefault();

    let url = btn.getAttribute("href")

    Swal.fire({
        title: 'Əminsiniz?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Bəli!',
        cancelButtonText: 'Xeyr!'
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(url)
                .then(res => {
                    if (res.status == 200) {
                        Swal.fire({
                            title: 'Uğurla qaytarıldı!',
                            icon: 'success',
                        }).then(answer => {
                            if (answer.isConfirmed) {
                                window.location.reload(true);
                            }
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Qaytarılmadı!',
                            icon: 'error',
                        });
                    }
                })
        }
    })
}));