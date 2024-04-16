<script>
    $(document).ready(function () {
    // Initialize an array to store table data
    var tables = @Html.Raw(Json.Serialize(Model.Table));

    // Function to populate the grid with items
    function populateGrid() {
        // Clear the grid container
        $("#gridContainer").empty();

    // Populate the grid with items from the tables array
    tables.forEach(function (table) {
            // Create a grid item
            var gridItem = $("<div class='grid-item'><a href='#' class='' data-table-id='" + table.tableId + "' style='color: black;font-size: 18px'>Table: " + table.tableId + "</a></div>");

    if (table.isReserved) {
        gridItem.addClass('reserved');
            } else {
        gridItem.addClass('available');
    gridItem.addClass('addTableButton');
            }

    // Add click event handler to select the item
    gridItem.click(function () {
        // Example: perform some action with the selected item
        $("#tableIdInput").val(table.tableId);
    console.log("Selected table:", table);
            });

    // Append the grid item to the grid container
    $("#gridContainer").append(gridItem);
        });
    }



    var tableGroup = true;
    // Event handler for opening the popup
    $("#openPopupButton").click(function () {

        if (tableGroup) {
        tableGroup = false;
    $("#popupModal").show();
        } else {
        tableGroup = true;
    $("#popupModal").hide();
        }

    // Call a function to populate the grid with items
    populateGrid();
    });

    $("#openPopupButton").click(function (event) {
        event.preventDefault(); // Prevent the default behavior of the button click
        // Your existing code to open the popup modal and populate the grid
    });



    $(".addTableButton").click(function () {
        // Get the table ID associated with the button
        var tableId = $(this).data("table-id");

    var table = tables.find(function (t) {
            return t.tableId === tableId;
        });

    // Serialize the table object and set it as the value of the input field
    var serializedTable = JSON.stringify(table);
    $("#hiddenInput").val(serializedTable);

    });

});

</script>


/*        <div class="form-group">
    <input asp-for="Table" type="" id="hiddenInput" name="Table" />
    <span asp-validation-for="Table" class="text-danger"></span>
</div>
<!--Button to open the popup-- >
<button style="padding: 10px" class="button" id="openPopupButton">Open Popup</button>

<div style="padding: 10px" class="form-group">
    <label for="Table">Bàn đặt</label>
    <input class="form-control" id="tableIdInput" readonly>
</div>
<!--Popup modal-- >
    <div id="popupModal" style="display: none;padding: 10px">
        <div id="gridContainer" class="grid-container">
            <!-- Grid items will be populated here -->
        </div>
    </div>*/