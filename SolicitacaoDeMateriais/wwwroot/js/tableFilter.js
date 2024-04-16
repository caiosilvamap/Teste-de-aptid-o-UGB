
function filterTable() {
 
    var searchText = document.getElementById("search").value.toLowerCase();

    var rows = document.querySelectorAll("#dataTable tbody tr");

    rows.forEach(function (row) {

        var cells = row.querySelectorAll("td");
        var found = false;

        cells.forEach(function (cell) {

            if (cell.textContent.toLowerCase().indexOf(searchText) !== -1) {

                found = true;
            }
        });

        if (found) {
            row.style.display = "";
        } else {
            row.style.display = "none";
        }
    });
}


document.querySelector("#searchButton").addEventListener("click", filterTable);
