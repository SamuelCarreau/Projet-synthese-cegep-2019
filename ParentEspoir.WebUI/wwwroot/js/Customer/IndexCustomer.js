let href = '/?currentPage=1&searchFilter=';
function changeSearchFilterValue() {
    href = href + $('#searchInput').val();
}
function redirectWithSearch() {
    window.location.href = href;
    href = '/?currentPage=1&searchFilter=';
}

//function handleEnter() {
//    if (event.key === "Enter") {
//        changeSearchFilterValue();
//        redirectWithSearch();
//    }
//}

$('#searchInput').keydown(function (event) {
    if (event.key === "Enter") {
        changeSearchFilterValue();
        redirectWithSearch();
    }
})