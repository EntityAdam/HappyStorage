'usestrict'

function toggleDrawer() {
    var drawer = document.getElementById('sidebar');
    drawer.classList.toggle('active');

    let icons = document.querySelectorAll('#sidebar ul li i');
    Array.from(icons, x => x.classList.add('stuff'));
}


