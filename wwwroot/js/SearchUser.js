$(document).ready(function () {
    var minlength = 2;
    var searchRequest = null;
    var res = null;
    var ul = document.getElementById("searchResultList");
    $("#userSearch").keyup(function () {
        var that = this,
            value = $(this).val();
        if (value.length >= minlength) {
            if (searchRequest != null)
                searchRequest.abort();
            searchRequest = $.ajax({
                type: "POST",
                url: "/api/user/search",
                data: {
                    'search_keyword': value
                },
                dataType: "text",
                success: function (msg) {
                    //we need to check if the value is the same
                    if (value == $(that).val()) {
                        //Receiving the result of search here
                        if (msg.length > 0) {

                            res = JSON.parse(msg)
                            console.log(typeof (res))
                            console.log(res.data);
                            var result = res.data;
                            ul.innerHTML = "";
                            for (var i = 0; i < result.length; i++) {
                                var uName = result[i].userName;
                                var fName = result[i].firstName;
                                var loc = result[i].location;
                                var pUri = "profile.jpg";
                                console.log(pUri)
                                var htmlElem = createmyElements(uName, fName, loc, pUri)

                                var li = document.createElement("li");
                                console.log(htmlElem)
                                li.innerHTML = htmlElem;
                                li.style.cssText = "margin-top:5px;"
                                ul.appendChild(li);
                            }
                        }
                        else {

                        }
                    }
                }
            });
        }
        else {
            res = null;
            ul.innerHTML = "";
        }
    });


    function createmyElements(uName, fName, loc, pUri) {

        var myvar =
            '  <div class="card mx-auto">' +
            '      <div class="row mt-4 mb-3">' +
            '          <div class="col-md-2 col-2 text-right pl-4">' +
            '              <img class="ml-4 mb-2 rounded-circle z-depth-2" height="70" width="70" alt="100x100"' +
            '                   src="/images/' + pUri + '"' +
            '                   data-holder-rendered="true">' +
            '          </div>' +
            '          <div class="col-md-5 col-5 text-left mt-2 p-0">' +
            '              <h6 class="ml-4 mb-2 font-weight-bold">' + fName + '(<span style="color:mediumpurple">' + uName + '</span>)</h6>' +
            '              <h6 class="ml-4 mb-2 font-small grey-text mb-1"><i class="fas fa-map-marker-alt pr-1" aria-hidden="true"></i>' + loc + '</h6>' +
            '          </div>' +
            '          <div class="col-md-5 col-5 text-right pr-5">' +
            '              <button type="button" class="btn  btn-primary">Add Friend</button>' +
            '              <button type="button" class="btn  btn-primary">View Profile</button>' +
            '          </div>' +
            '      </div>' +
            '  </div>';

        return myvar;
    }
});