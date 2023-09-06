function EditData() {
    document.getElementById('AgencyName').disabled = false;
    document.getElementById('FirstName').disabled = false;
    document.getElementById('LastName').disabled = false;
    document.getElementById('Username').disabled = false;
    document.getElementById('Password').disabled = false;
    document.getElementById('ConfirmPassword').hidden = "";
    document.getElementById('ConfirmPassword').disabled = false;
    document.getElementById('Phone').disabled = false;

    document.getElementById('EditBtn').hidden = "hidden";
    document.getElementById('SaveBtn').hidden = "";
}