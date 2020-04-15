

var globalObjectPointer = {};  //hasmap for model name based mapping since outside protp ??
var globalBaseHtmlPointer = {};

function jGrid(base, caption, action, filter) {
    this.action = action;
    this.filter = filter;
    this.jgridBase = base;
    this.gridCaption = caption;
    this.actionURL = action;

    this.isVerifiable = {};
    this.isEditable = {};
    this.isDeletable = {};
    this.isFlexible = {};
    this.isCustomTableHtml = {};// = false; //{} // not working since div becoming Table // need to re think 

    this.columnName = []; //{}
    this.columnWidth = []; //{}

    this.onMasterItemClick = null;

    this.isMasterRecordEditDeleteClicked = false; //{}
    this.masterRecordKeyField = '';
    this.currentMasterRecordId = '';
    this.currentModelName = '';  //{}
    this.currentModelType = '';  //{}
    this.masterTemplate = '';
    this.masterModelName = '';
    this.detailModelName = '';   //{}
    this.verifyModels = '';  //{}
    this.isInvokeList = {};

    this.masterCurrentPage = 1; //x
    this.pagingSize = 15; //x
    this.gridHeight = 500; //x
    this.masterModalWidth = 700; //x
    this.detailModalWidth = 750; //x
    this.manageItemsColWidth = 13; //x

    this.isFirstCall = true; // {}
    this.isSinglePaging = {};

    this.selctedMasterJsonData = null;
    globalObjectPointer[base] = this;  ///replase later with model name

    this.isVerifyByMaster = false;
    this.showListModel = {};
    this.masterInner = '';
    this.detailInner = '';
    this.detailList = [];    
}

jGrid.prototype.setShowListModel = function (modelName, listModel) {
    this.showListModel[modelName] = listModel;
}


jGrid.prototype.setPageSize = function (pageSize) {
    if (typeof pageSize !== 'undefined' && pageSize != null) this.pagingSize = pageSize;
}

jGrid.prototype.setGridHeight = function (height) {
    if (typeof height !== 'undefined' && height != null) this.gridHeight = height;
}

jGrid.prototype.setModalWidth = function (masterWidth, detailWidth) { //check null or undefine
    this.masterModalWidth = masterWidth;
    this.detailModalWidth = detailWidth;
}

jGrid.prototype.setColumnName = function (columns) {
    this.columnName = columns;
}

jGrid.prototype.setColumnWidth = function (columns) {
    this.columnWidth = columns;
}

var imgURL = '';
jGrid.prototype.setImageBackground = function (url) {
    imgURL = url;
}

function setBackground() {
    if (imgURL != '') {
        $('.j-grid-master-caption').css('background-image', imgURL);
        $('.j-grid-detail-caption').css('background-image', imgURL);
        $('.j-grid-master-paging').css('background-image', imgURL);
        $('.j-grid-detail-base').css('background-image', imgURL);
    }
}

// Set base HTML layout
jGrid.prototype.init = function (isSingle) {
    $('.j-master').prop('visible', 'true');
    $('.j-detail').prop('visible', 'true');
    $(this.base).show();
    if (globalBaseHtmlPointer[this.jgridBase] == null)
        globalBaseHtmlPointer[this.jgridBase] = $('#' + this.jgridBase).html();
    else
        $('#' + this.jgridBase).html(globalBaseHtmlPointer[this.jgridBase]);
    
    //$masterInner = '', $detailInner = '';

    //$jgrid = $('.j-grid');
    //if (this.baseHTML.length == 0) this.baseHTML = $('.j-grid').html();
    //$('.j-grid').empty();
    //$('.j-grid').html(this.baseHTML);

    $jgrid = $('#' + this.jgridBase);
    //if (this.baseHTML == '') this.baseHTML = $('#' + this.jgridBase).html();
    //else $('#' + this.jgridBase).html(this.baseHTML);

    //if ($currentGrid != this.jgridBase) this.baseHTML = $('#' + this.jgridBase).html();
    //$('#' + this.jgridBase).empty();
    //$('#' + this.jgridBase).html(this.baseHTML);

    //var masterBase = $jgrid.children(":first").hasClass("j-master");
    //var detailBase = $jgrid.children(":nth-child(2)").hasClass("j-detail");
    //if (typeof $('.j-master')[0] !== 'undefined' && $('.j-master')[0] != null) $masterInner = $jgrid.children(":first").html(); //$('.j-master')[0].innerHTML;  //Im
    //if (typeof $('.j-detail')[0] !== 'undefined' && $('.j-detail')[0] != null) $detailInner = $jgrid.children(":first").html(); //$('.j-detail')[0].innerHTML;  //Im //:nth-child(2)

    if (isSingle) this.detailInner = $jgrid.children(":first").html();
    else {
        this.masterInner = $jgrid.children(":first").html();
        this.detailInner = $jgrid.children(":nth-child(2)").html();
    }

    var jhtmlSingle = '<div id="' + this.jgridBase + '"><div class="card-style">' +
        '<div class="col-md-12" id="j-loader" style="width:100%;height:100%;border-radius: 5px;"><div id="spinner"><img alt="" src="/images/j-loader.gif"></div></div>' +
        '        <div class="col-md-12" style="padding:0;">' +
        '           <div class="j-grid-detail-layout" style="border: #ededed solid 2px;">' +
        '               <div class="j-grid-detail-caption" id="DetailsCaption" style="background-color: #ededed;border-top-left-radius: 4px;border-top-right-radius: 4px;">' + this.gridCaption + '</div>' +
        '               <div id="content' + this.jgridBase + '" style="padding:5px;">' +
                        this.detailInner +
        '               </div>' +

        '              <div id="pagingBase">' +
        '              <div class="pagination-holder j-grid-master-paging" style="width: 40%;background-color: #ededed;border-bottom-left-radius: 4px;border-bottom-right-radius: 4px;">' +
        '                   <div style="display:inline-block;width:68%;">' +
        '                        <div id="light-pagination-single' + this.jgridBase + '" class="pagination light-theme simple-pagination">' +
        '                        </div>' +
        '                   </div>' +
        '                    <div style="display:inline-block;width:30%;text-align:right;vertical-align: top;padding-top: 6px;font-weight: bold;font-size: 12px">' +
        '                        Total Record : <span id="TotalRecord">200</span>' +
        '                    </div>' +
        '               </div>' +


        '               <div class="j-grid-single-verification-base" style="width:60%;">' +
        '                   <div class="col-md-2" style="padding:0">' +
        '                       <div class="j-grid-created-by"><span id="lblCreated"></span></div>' +
        '                   </div>' +
        '                   <div class="col-md-2" style="padding:0">' +
        '                       <div class="j-grid-verified-by"><span id="lblVerified"></span></div>' +
        '                   </div>' +
        '                   <div class="col-md-5" style="padding:0;margin-top:-1px;">' +
        '                       <div style="padding-right:5px;padding-left:5px;"><input type="text" id="VerificationNote" placeholder="Verification Note" style="font-size:13px;width:100%;height:33px;" class="k-textbox" /></div>' +
        '                   </div>' +
        '                   <div class="col-md-3" style="padding:0;margin-top:-1px;">' +
        '                        <div style="padding-right:0px;padding-left:5px; width:45%;display:inline-block;vertical-align:top;text-align:center;">' +
        '                            <div class="j-grid-created-by"><span>Status<br /><span id="lblStatus"></span></span></div>' +
        '                        </div>' +
        '                       <div style="width:50%;display:inline-block;">' +
        '                            <div class="btn-group dropup" style="width:105%;">' +
        '                                <button type="button" class="btn btn-success" data-toggle="dropdown" style="width:100%;">' +
        '                                    Verify' +
        '                                </button>' +
        '                                <ul class="dropdown-menu" style="min-width: 100%;">' +
        '                                    <li><a id="1" class="linkButton" onClick="onRecordVerifyProxy(\'' + this.jgridBase + '\',this.id)">Verify Selected</a></li>' +
        '                                    <li><a id="2" class="linkButton" onClick="onRecordVerifyProxy(\'' + this.jgridBase + '\',this.id)">Reject Selected</a></li>' +
        '                                    <li><a id="0" class="linkButton" onClick="onRecordVerifyProxy(\'' + this.jgridBase + '\',this.id)">Reset Selected</a></li>' +
        '                                </ul>' +
        '                            </div>' +
        '                        </div>' +
        '                    </div>' +
        '           </div>' +
        '               </div>' +
        '           </div>' +
        '        </div>' +
        '        </div></div>' +

        //Edit modal template
        '   <div class="modal fade" id="editModal" style="z-index:999999999999999;padding-top:50px">' +
        '    <input type="text" id="PK" name="PK" hidden="hidden" />' +
        '    <div id="modalContent">' +
        '        <div id="editDialogBase" class="modal-dialog">' +
        '            <div class="modal-content">' +
        '                <div class="modal-header">' +
        '                    <button type="button" class="close"' +
        '                            data-dismiss="modal" aria-label="Close">' +
        '                        <span aria-hidden="true">&times;</span>' +
        '                    </button>' +
        '                    <h4 class="modal-title"><span id="modalTitle">t</span></h4>' +
        '                </div>' +
        '                <div class="modal-body">' +
        '                </div>' +
        '                <div class="modal-footer">' +
        '                    <button type="button" class="btn" data-dismiss="modal">Close</button>' +
        '                    <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="onRecordEditUpdateProxy(' + this.jgridBase + ')">Save changes</button>' +
        '                </div>' +
        '            </div>' +
        '        </div>' +
        '    </div>' +
        '</div>' +

        //Map modal template
        '<div class="modal fade" id="mapModal">' +
        '    <div id="mapDialogBase" class="modal-dialog" style="width:800px;">' +
        '        <div class="modal-content">' +
        '            <div class="modal-header">' +
        '                <button type="button" class="close"' +
        '                        data-dismiss="modal" aria-label="Close">' +
        '                    <span aria-hidden="true">&times;</span>' +
        '                </button>' +
        '                <h4 class="modal-title"><span id="mapModalTitle">Service Location</span></h4>' +
        '            </div>' +
        '            <div class="modal-body"><div id="map-canvas"></div></div>' +
        '            <div class="modal-footer">' +
        '                <button type="button" class="btn" data-dismiss="modal">Close</button>' +
        '            </div>' +
        '        </div>' +
        '    </div>' +
        '</div>';




    var jhtmlMulti = '<div id="' + this.jgridBase + '"><div class="card-style">' +
        '<div class="container" style="padding-bottom:15px;padding-top: 15px;">' +
        '<div class="col-md-12" id="j-loader"><div id="spinner"><img alt="" src="/images/j-loader.gif"></div></div>' +
        //'<div id="preloader"><div id="spinner"><img alt="" src="images/preloaders/4.gif"></div></div>'+
        '<div class="col-md-4" style="padding:0;">' +
        '           <div class="j-grid-master-layout">' +
        '             <div class="j-grid-master-caption">' + this.gridCaption + '</div>' +
        '             <span class="export" onclick="onDataExportProxy(' + this.jgridBase + ',1)">Export</span>' +
        '          <div id="contentMaster' + this.jgridBase + '" style="padding:5px;">' +
                    this.masterInner +
        '           </div>' +
        '              <div class="pagination-holder j-grid-master-paging">' +
        '                   <div style="display:inline-block;width:68%;">' +
        '                        <div id="light-pagination' + this.jgridBase + '" class="pagination light-theme simple-pagination">' +
        '                        </div>' +
        '                   </div>' +
        '                    <div style="display:inline-block;width:30%;text-align:right;vertical-align: top;padding-top: 6px;font-weight: bold;font-size: 12px">' +
        '                        Total Record : <span id="TotalRecord">200</span>' +
        '                    </div>' +
        '               </div>' +
        '           </div>' +
        '       </div>' +

        '       <div class="col-md-8" style="padding:0;">' +
        '           <div class="j-grid-detail-layout">' +
        '               <div class="j-grid-detail-caption" id="DetailsCaption"></div>' +
        '               <span class="export" onclick="showMapProxy(' + this.jgridBase + ')" style="right: 3px;">Map</span>' +
        '               <div id="content' + this.jgridBase + '" style="padding:5px;">' +
                        this.detailInner +
        '               </div>' +
        '               <div class="j-grid-detail-base">' +
        '                   <div class="col-md-2" style="padding:0">' +
        '                       <div class="j-grid-created-by"><span id="lblCreated"></span></div>' +
        '                   </div>' +
        '                   <div class="col-md-2" style="padding:0">' +
        '                       <div class="j-grid-verified-by"><span id="lblVerified"></span></div>' +
        '                   </div>' +
        '                   <div class="col-md-5" style="padding:0;margin-top:-1px;">' +
        '                       <div style="padding-right:5px;padding-left:5px;"><input type="text" id="VerificationNote" placeholder="Verification Note" style="font-size:13px;width:100%;height:33px;" class="k-textbox" /></div>' +
        '                   </div>' +
        '                   <div class="col-md-3" style="padding:0;margin-top:-1px;">' +
        '                        <div style="padding-right:0px;padding-left:5px; width:45%;display:inline-block;vertical-align:top;text-align:center;">' +
        '                            <div class="j-grid-created-by"><span>Status<br /><span id="lblStatus"></span></span></div>' +
        '                        </div>' +
        '                       <div style="width:50%;display:inline-block;">' +
        '                            <div class="btn-group dropup" style="width:105%;">' +
        '                                <button type="button" class="btn btn-success" data-toggle="dropdown" style="width:100%;">' +
        '                                    Verify' +
        '                                </button>' +
        '                                <ul class="dropdown-menu" style="min-width: 100%;">' +
        '                                    <li><a id="1" class="linkButton" onClick="onRecordVerifyProxy(\'' + this.jgridBase + '\',this.id)">Verify Selected</a></li>' +
        '                                    <li><a id="2" class="linkButton" onClick="onRecordVerifyProxy(\'' + this.jgridBase + '\',this.id)">Reject Selected</a></li>' +
        '                                    <li><a id="0" class="linkButton" onClick="onRecordVerifyProxy(\'' + this.jgridBase + '\',this.id)">Reset Selected</a></li>' +
        '                                </ul>' +
        '                            </div>' +
        '                        </div>' +
        '                    </div>' +
        '           </div>' +
        '   </div>' +
        '</div></div>' +


        //Edit modal template
        '   <div class="modal fade" id="editModal">' +
        '    <input type="text" id="PK" name="PK" hidden="hidden" />' +
        '    <div id="modalContent">' +
        '        <div id="editDialogBase" class="modal-dialog">' +
        '            <div class="modal-content">' +
        '                <div class="modal-header">' +
        '                    <button type="button" class="close"' +
        '                            data-dismiss="modal" aria-label="Close">' +
        '                        <span aria-hidden="true">&times;</span>' +
        '                    </button>' +
        '                    <h4 class="modal-title"><span id="modalTitle">t</span></h4>' +
        '                </div>' +
        '                <div class="modal-body">' +
        '                </div>' +
        '                <div class="modal-footer">' +
        '                    <button type="button" class="btn" data-dismiss="modal">Close</button>' +
        '                    <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="onRecordEditUpdateProxy(' + this.jgridBase + ')">Save changes</button>' +
        '                </div>' +
        '            </div>' +
        '        </div>' +
        '    </div>' +
        '</div>' +

        //Map modal template
        '<div class="modal fade" id="mapModal">' +
        '    <div id="mapDialogBase" class="modal-dialog" style="width:800px;">' +
        '        <div class="modal-content">' +
        '            <div class="modal-header">' +
        '                <button type="button" class="close"' +
        '                        data-dismiss="modal" aria-label="Close">' +
        '                    <span aria-hidden="true">&times;</span>' +
        '                </button>' +
        '                <h4 class="modal-title"><span id="mapModalTitle">Service Location</span></h4>' +
        '            </div>' +
        '            <div class="modal-body"><div id="map-canvas"></div></div>' +
        '            <div class="modal-footer">' +
        '                <button type="button" class="btn" data-dismiss="modal">Close</button>' +
        '            </div>' +
        '        </div>' +
        '    </div>' +
        '</div>';

    $jgrid.replaceWith(isSingle ? jhtmlSingle : jhtmlMulti);

    var $preloader = $('#j-loader');
    if ($preloader.length > 0) {
        $preloader.delay(700).fadeOut('slow');
    }
}

jGrid.prototype.setSlimScroll = function (modelName) {
    if (modelName.length > 0) {
        $('#' + modelName).slimScroll({
            height: (this.gridHeight - 75) + 'px'
        });
    }
    $('#content' + this.jgridBase).slimScroll({
        height: (this.gridHeight - 70) + 'px'
    });

    $('.j-grid-master-layout').css('min-height', this.gridHeight + 'px');
    $('.j-grid-detail-layout').css('min-height', this.gridHeight + 'px');
    $(document.getElementsByClassName("slimScrollBarX")).css('display', 'none');
}

// Set paging
jGrid.prototype.createPaging = function (op, isMaster, base, totalRecord) {
    var _showPageNumbers, _showNavigator, _showGoInput, _showGoButton;
    if (isMaster) _showPageNumbers = false, _showNavigator = true, _showGoInput = true, _showGoButton = true; else _showPageNumbers = true, _showNavigator = false, _showGoInput = false, _showGoButton = false;

    var container = $('#' + base).pagination({
        dataSource: function (done) {
            var result = [];
            for (var i = 1; i < totalRecord; i++) {
                result.push(i);
            }
            done(result);
        },
        pageSize: this.pagingSize,
        showPageNumbers: _showPageNumbers,
        showNavigator: _showNavigator,
        showGoInput: _showGoInput,
        showGoButton: _showGoButton,
        triggerPagingOnInit: false,
        callback: function (data, pagination) {
            $isFirstCall = true;
            if (pagination.direction != 0) {
                var parm;
                if (isMaster) {
                    parm = {
                        "ModelName": op.masterModelName,
                        "RequestType": "Show",
                        "RecordType": "Master",
                        "PageSize": op.pagingSize,
                        "PageNo": pagination.pageNumber,
                        "Data": JSON.stringify(op.filter)
                    };
                    op.callMasterAction(parm, op.masterModelName, op.masterTemplate, op.onMasterItemClick);
                    var d = 0;
                } else {
                    parm = {
                        "ModelName": op.detailModelName,
                        "RequestType": "Show",
                        "RecordType": "Detail",
                        "RecordId": op.currentMasterRecordId,
                        "PageSize": op.pagingSize,
                        "PageNo": pagination.pageNumber,
                        "Data": JSON.stringify(op.filter)
                    };
                    op.callDetailAction(op.detailModelName, parm, op.jgridBase);
                }
            }
            op.masterCurrentPage = pagination.pageNumber;
        }
    });
    container.pagination('go', this.masterCurrentPage);
    $("#TotalRecord").text(totalRecord);
}

//Clear the Details div and set templates
jGrid.prototype.clearDetail = function () {
    $('#content' + this.jgridBase).empty();
    $('#content' + this.jgridBase).html(this.detailInner);
}

//Clear the Master div and set templates
jGrid.prototype.clearMaster = function () {
    $('#contentMaster' + this.jgridBase).empty();
    $('#contentMaster' + this.jgridBase).html(this.masterInner);
}

// Bind master div
jGrid.prototype.bindMaster = function (modelName, templateId, callbackFn, isVerifyMaster) {

    this.init(false);

    if (typeof isVerifyMaster !== 'undefined' && isVerifyMaster != null) this.isVerifyByMaster = isVerifyMaster;
    this.masterModelName = modelName;
    this.masterTemplate = templateId;
    this.setSlimScroll(modelName);

    var parm = { "ModelName": modelName, "RequestType": "Show", "RecordType": "Master", "PageSize": this.pagingSize, "PageNo": 1, "Data": JSON.stringify(this.filter) }; //Changed Data to Filter -Belayet
    this.callMasterAction(parm, modelName, templateId, callbackFn);
}

jGrid.prototype.callMasterAction = function (parm, modelName, templateId, callbackFn) {

    var op = this;
    appetizer.actionCall(this.actionURL, parm, "GET", function (data, result) {
        if (result) {
            appetizer.ajaxLoder.hidePleaseWait();
            var serverReturn = data.Data;  //this.currentMasterRecordId = ''; // Why??            
            op.createMasterItems(modelName, templateId, serverReturn.Record, callbackFn);
            var total = 0;
            if (serverReturn.Record.length > 0)
                total = serverReturn.Record[0].TotalRecord;

            op.createPaging(op, true, 'light-pagination' + op.jgridBase, total);
        }
        else {
            appetizer.ajaxLoder.hidePleaseWait();
            appetizer.message.showError('#showErrorMsg', data.Data, 10000);
        }
    });
}

// Create master div items
jGrid.prototype.createMasterItems = function (modelName, templateId, data, callbackFn) {

    var op = this;

    if (this.isFirstCall) {
        this.selctedMasterJsonData = data[0];
        this.currentMasterRecordId = '';
    }

    $("#" + modelName).html('');
    for (var i = 0; i < data.length; i++) {
        var clon = document.getElementById(templateId).content.cloneNode(true);

        this.masterRecordKeyField = clon.children[0].className;
        clon.children[0].id = data[i][this.masterRecordKeyField];

        $(clon.children[0]).attr('data-json', encodeURIComponent(JSON.stringify(data[i])));
        clon.children[0].addEventListener('click', function (event) {
            op.currentMasterRecordId = this.id;
            op.selctedMasterJsonData = JSON.parse(decodeURIComponent(this.getAttribute("data-json")));

            //this.setVerificationInfo();  //--------Commented By Belayet
            //setActiveRow(); //--------Commented By Belayet

            //Modified By Belayet
            if (!op.isMasterRecordEditDeleteClicked) {
                op.onMasterItemClick(this.id);
                op.setVerificationInfo();  // Remove
                if (op.isVerifyByMaster) op.showVerifyInfo(modelName + '-M', this.id);
                op.setActiveRow();
            } else op.isMasterRecordEditDeleteClicked = false;
        });

        for (var key in data[i]) {
            if (key != "") {
                var fieldName = key;
                var cellValue = (data[i][key]);
                var node = clon.getElementById(fieldName);
                if (fieldName == this.masterRecordKeyField) {
                    var e = clon.getElementById("edit");
                    var d = clon.getElementById("delete");
                    $(e).attr('id', cellValue);
                    $(d).attr('id', cellValue);
                    $(e).click(function (event) { op.onRecordEdit(this.id, modelName, true); });
                    $(d).click(function (event) { op.onRecordDelete(this.id, modelName, true, op.onMasterItemClick); });
                    if (this.currentMasterRecordId.length == 0) this.currentMasterRecordId = cellValue;
                }

                if (node != null) {
                    if ($(node).attr("data-type") == 'color') { $(node).css({ "background-color": cellValue }); }
                    else if ($(node).attr("data-type") == 'id') { $(node).attr('id', cellValue); }
                    else { node.innerHTML = cellValue; }
                }
            }

        }
        $("#" + modelName).append(clon);
    }

    if (data.length == 0) {
        $("#" + modelName).html('<div style="width:100%;border:1px solid #ddd;text-align:center;font-size:14px;color:#c20123;font-weight:500;padding-top:50px;padding-bottom:50px;">Oops, Sorry no data found!</div>');
        op.clearDetail();
        //for (var i = 0; i < detailList.length; i++) {
        //    var isCustomTable = false;
        //    if (document.getElementById(detailList[i]).nodeName.toUpperCase().includes('DIV')) isCustomTable = false;
        //    if (document.getElementById(detailList[i]).nodeName.toUpperCase().includes('TABLE')) isCustomTable = true;
        //    if (!isCustomTable) {
        //        $('#' + detailList[i]).html('');
        //    }
        //    else {
        //        var numCols = $("#" + detailList[i]).find('tr')[0].cells.length + 1;
        //        $('#' + detailList[i]).find('tbody').html('<tr><td colspan="' + numCols + '"style="background-color:#fff;border: 1px solid #fff;width:100%;text-align:center;font-size:14px;color:#c20123;font-weight:500;padding-top:50px;padding-bottom:50px">Oops, Sorry no data found!</td></tr>');
        //    }
        //}
        $('#DetailsCaption').html('<div>&nbsp;</div>');
        $('#lblCreated').html('Created by ');
        $('#lblVerified').html('Verified by');
        $('.linkButton').addClass('hidden');
    }
    else {
        $('.linkButton').removeClass('hidden');
    }

    if (data.length > 0) callbackFn(this.currentMasterRecordId);
    this.onMasterItemClick = callbackFn;
}

function onRecordEditUpdateProxy(base) {
    globalObjectPointer[base.id].onRecordEditUpdate(globalObjectPointer[base.id].onMasterItemClick); /// globalObjectMap
}

// Invoke after edit save
jGrid.prototype.onRecordEditUpdate = function (notifyGrid) {

    var data = appetizer.div.getJsonObjectFromDiv("modalContent", '0');

    var recordId = $("#PK").val();
    var parm = { "ModelName": this.getEntityModel(this.currentModelName), "RequestType": "Update", "RecordType": this.currentModelType, "RecordId": recordId, "Data": JSON.stringify(data) };

    var op = this;
    appetizer.actionCall(this.actionURL, parm, "GET", function (data, result) {
        if (result) {
            //op.isFirstCall = false; //[tihis was creating problem]/after edit save page navigation not working ?????? //BLOCKED by Imrose
            if (this.currentModelType == "Master") {
                var parm = { "ModelName": op.masterModelName, "RequestType": "Show", "RecordType": "Master", "PageSize": op.pagingSize, "PageNo": op.masterCurrentPage, "Data": JSON.stringify(op.filter) };
                op.callMasterAction(parm, op.masterModelName, op.masterTemplate, op.onMasterItemClick);
            }
            else {
                //notifyGrid(op.currentMasterRecordId);
                if (notifyGrid != null) notifyGrid(op.currentMasterRecordId);
                else {
                    parm = { "ModelName": op.currentModelName, "RequestType": "Show", "RecordType": "Detail", "RecordId": op.currentMasterRecordId, "PageSize": op.pagingSize, "PageNo": 1, "Data": JSON.stringify(op.filter) };
                    op.callDetailAction(op.currentModelName, parm, op.jgridBase);
                }
            }
            appetizer.message.showInfo('#showMsg', 'Record updated successfully', 2500);
        }
        else {
            appetizer.ajaxLoder.hidePleaseWait();
            appetizer.message.showError('#showErrorMsg', data.Data, 10000);
        }
    });
}

function setVerifyStatus(base, o, modelName) {

    if ($(o).hasClass('verify-selected-blue')) {
        $(o).addClass($(o).attr('data-vc')).removeClass('verify-selected-blue');
    }
    else {
        var className = $(o).attr('class').split(' ');
        className = className[className.length - 1]; //dangerous as last index
        $(o).attr('data-vc', className);
        $(o).addClass('verify-selected-blue').removeClass(className);

        if (!globalObjectPointer[base].verifyModels.replace(modelName, '').includes('undefined'))
            globalObjectPointer[base].verifyModels = globalObjectPointer[base].verifyModels.replace(modelName, '').trimLeft() + modelName + ' ';
    }
    //globalObjectPointer[base].verifyModels = globalObjectPointer[base].verifyModels.trim();
    //alert(globalObjectPointer[base.id].gridCaption);
}

function onRecordEditProxy(base, id, modelName, isMaster) {
    globalObjectPointer[base.id].onRecordEdit(id, modelName, isMaster) /// globalObjectMap
}

jGrid.prototype.getEntityModel = function (modelName) {
    if (modelName.includes("View")) return this.masterModelName; else return modelName;
}

// Invoke edit modal
jGrid.prototype.onRecordEdit = function (id, modelName, isMaster) {

    //globalObjectPointer = this; /// globalObjectMap

    if (isMaster) this.isMasterRecordEditDeleteClicked = true;

    var recordType = isMaster ? 'Master' : 'Detail';
    var parm = { "ControllerName": this.masterModelName, "ModelName": this.getEntityModel(modelName), "RequestType": "ShowEdit", "RecordType": recordType, "RecordId": id };

    //if (controllerName == 'undefined' || controllerName == null) { controllerName = masterModelName }//Added By Belayet
    //var parm = { "ControllerName": controllerName /*masterModelName*/, "ModelName": modelName, "RequestType": "ShowEdit", "RecordType": recordType, "RecordId": id };

    var op = this;
    appetizer.actionCall(this.actionURL, parm, "GET", function (data, result) {
        if (result) {
            //appetizer.ajaxLoder.hidePleaseWait();
            op.currentModelName = modelName;
            if (isMaster) op.currentModelType = 'Master'; else op.currentModelType = 'Detail';

            $emodal = $("#editModal");
            $("#modalTitle").text(modelName.match(/[A-Z][a-z]+|[0-9]+/g).join(" ") + ' Information');
            document.getElementById("PK").value = id;
            let frag = document.createRange().createContextualFragment(data.html);
            $emodal.find("div.modal-body").html(frag);
            isMaster ? $('#editDialogBase').css('width', op.masterModalWidth) : $('#editDialogBase').css('width', op.detailModalWidth);

            appetizer.div.setJsonObjectToDiv(data.json, "modalContent");
            $emodal.modal("show");
        }
        else {
            appetizer.ajaxLoder.hidePleaseWait();
            appetizer.message.showError('#showErrorMsg', data.Data, 10000);
        }
    });
}

function onRecordDeleteProxy(base, id, modelName, isMaster, notifyGrid) {
    globalObjectPointer[base.id].onRecordDelete(id, modelName, isMaster, globalObjectPointer[base.id].onMasterItemClick); /// globalObjectMap
}

// Perform delete operation
jGrid.prototype.onRecordDelete = function (id, modelName, isMaster, notifyGrid) {
    if (isMaster) this.isMasterRecordEditDeleteClicked = true; //-----added by Belayet
    var selectedRow = document.getElementById(id);

    var master = null;
    if (selectedRow != null)
        master = JSON.parse(decodeURIComponent(selectedRow.getAttribute("data-json")));

    if (master != null) {
        if (master.IsVerified == "1") {
            appetizer.message.showWarning('#showErrorMsg', "You can not delete verified data.", 10000);
            return;
        }
    }

    //this.isMasterRecordEditDeleteClicked = true;//-----Commented By Belayet
    var recordType = isMaster ? 'Master' : 'Detail';
    var parm = { "ModelName": this.getEntityModel(modelName), "RequestType": "Delete", "RecordType": recordType, "RecordId": id };

    var op = this;
    appetizer.message.showConfirm("Once deleted, you will not be able to recover this record.", function (data) {
        if (data) {
            appetizer.actionCall(op.actionURL, parm, "GET", function (data, result) {
                if (result) {
                    if (isMaster) {
                        var parm = { "ModelName": op.masterModelName, "RequestType": "Show", "RecordType": "Master", "PageSize": op.pagingSize, "PageNo": op.masterCurrentPage, "Data": JSON.stringify(this.filter) };
                        op.callMasterAction(parm, op.masterModelName, op.masterTemplate, op.onMasterItemClick);
                    } else {
                        if (notifyGrid != null) notifyGrid(op.currentMasterRecordId);
                        else {
                            parm = { "ModelName": op.currentModelName, "RequestType": "Show", "RecordType": "Detail", "RecordId": op.currentMasterRecordId, "PageSize": op.pagingSize, "PageNo": 1, "Data": JSON.stringify(op.filter) };
                            op.callDetailAction(op.currentModelName, parm, op.jgridBase);
                        }
                    }
                    //notifyGrid(op.currentMasterRecordId);
                    appetizer.message.showInfo("#showMsg", "Record delete successful!", 2500);
                }
                else {
                    appetizer.message.showError('#showErrorMsg', data.Data, 10000);
                }
            });
        }
    });
}

// isVerify, isEdit, isDelete, /*columnwidths, columnnames*/ -- REPLACE with {}
// Bind detail div
jGrid.prototype.bindDetails = function (detailObject, data, isVerify, isEdit, isDelete, isFlex, isList, columnWidths, columnNames) {
    this.clearDetail();
    this.columnName = [];
    this.columnWidth = [];
    this.currentMasterRecordId = data;

    setBackground();
    this.setSlimScroll('');
    this.setVerificationInfo();
    this.setActiveRow();

    var isCustomTable = false;
    if (typeof isFlex === 'undefined' || isFlex == null) this.isFlexible[detailObject] = false; else this.isFlexible[detailObject] = isFlex;
    if (typeof isVerify === 'undefined' || isVerify == null) this.isVerifiable[detailObject] = false; else this.isVerifiable[detailObject] = isVerify;
    if (this.isVerifyByMaster) this.isVerifiable[detailObject] = false;
    if (typeof isEdit === 'undefined' || isEdit == null) this.isEditable[detailObject] = false; else this.isEditable[detailObject] = isEdit;
    if (typeof isDelete === 'undefined' || isDelete == null) this.isDeletable[detailObject] = false; else this.isDeletable[detailObject] = isDelete;
    if (typeof isList === 'undefined' || isList == null) this.isInvokeList[detailObject] = false; else this.isInvokeList[detailObject] = isList;

    if (document.getElementById(detailObject).nodeName.toUpperCase().includes('DIV')) isCustomTable = false;
    if (document.getElementById(detailObject).nodeName.toUpperCase().includes('TABLE')) isCustomTable = true;
    this.isCustomTableHtml[detailObject] = isCustomTable;

    if (!isCustomTable) {
        if (typeof columnWidths !== 'undefined' && columnWidths != null) {
            if (typeof columnNames !== 'undefined' && columnNames != null) {
                this.columnName = columnNames;
                this.columnWidth = columnWidths;
            }
        }
    }

    //this.isVerifiable = isVerify; this.isEditable = isEdit; this.isDeletable = isDelete;
    var parm = { "ModelName": detailObject, "RequestType": "Show", "RecordType": "Detail", "RecordId": data };

    var op = this;
    appetizer.actionCall(this.action, parm, "GET", function (data, result) {
        if (result) {
            var serverReturn = data.Data;
            if (isCustomTable) op.custom(detailObject, serverReturn.Record); else op.auto(detailObject, serverReturn.Record);
        }
        else {
            appetizer.message.showError('#showErrorMsg', data.Data, 10000);
        }
    });
}



// Bind Custom div
jGrid.prototype.bindCustomDiv = function (detailObject, data) {

    this.currentMasterRecordId = data;
    var parm = { "ModelName": detailObject, "RequestType": "Show", "RecordType": "Detail", "RecordId": data };

    var op = this;
    appetizer.actionCall(this.action, parm, "GET", function (data, result) {
        if (result) {
            var serverReturn = data.Data;
            op.setCustomDiv(detailObject,serverReturn.Record);
        }
        else {
            appetizer.message.showError('#showErrorMsg', data.Data, 10000);
        }
    });
}

jGrid.prototype.setCustomDiv = function (modelName, data) {
    for (var i = 0; i < data.length; i++) {
        for (var key in data[i]) {
            $('#' + modelName + ' #' + key).html(data[i][key]);
        }
    }
}

// Create custom table
jGrid.prototype.custom = function (modelName, data) {
    var col = [];
    for (var i = 0; i < data.length; i++) {
        for (var key in data[i]) {
            if (col.indexOf(key) === -1) {
                col.push(key);
            }

        }
    }
    if (this.isVerifiable[modelName] || this.isEditable[modelName] || this.isDeletable[modelName]) col.push('Manage');

    $('#' + modelName).wrap('<div id="' + modelName + 'FixedBase"></div>');



    var table = document.getElementById(modelName);
    //console.log(table);
    var tbody = table.getElementsByTagName('tbody')[0];

    tbody.innerHTML = '';
    table.className = "table table-bordered table-striped table-hover";

    var tr = tbody.insertRow(-1); // TABLE ROW.

    this.addDataToTable(data, tbody, tr, col, modelName);

    if (this.isFlexible[modelName]) {
        $(table).css('white-space', 'nowrap');
        this.flex(modelName);//$('#' + modelName).parent().attr('id'));
    }

    this.setDetailOnClick(modelName);

    //if (this.isVerifiable[modelName]) this.addManageMenu(table, modelName);
}

// ADD JSON DATA TO THE TABLE AS ROWS.
jGrid.prototype.addDataToTable = function (data, tbody, tr, col, modelName) {
    if ($(tbody).prop("tagName").toUpperCase().includes('TABLE')) var table = tbody; else var table = $(tbody).parent();
    for (var i = 0; i < data.length; i++) {
        tr = tbody.insertRow(-1);

        var tabCell;
        for (var j = 0; j < col.length; j++) {

            if (!col[j].includes('Color')) {
                tabCell = tr.insertCell(-1);
                tabCell.innerHTML = data[i][col[j]];
                $(tabCell).attr("id", col[j] + i);

                if (col[j] == 'IsVerified' || col[j] == 'TotalRecord')//=======Added By Belayet                
                    $(tabCell).css("display", "none");//=======Added By Belayet 

                if (col[j] == 'Manage')//=======Added By Belayet    
                    if (this.isFlexible[modelName])
                        $(tabCell).addClass("td_manage");//=======Added By Belayet 

            } else {
                var fld = col[j].replace("Color", "");
                var f = document.getElementById(fld + i);
                if (data[i][col[j]] != "#fff") $(f).css("background-color", data[i][col[j]]);
            }

            //$(tabCell).css("text-align", "left");
            if (j == 0) $(tabCell).css("display", "none");
        }
        if (this.isVerifiable[modelName] || this.isEditable[modelName] || this.isDeletable[modelName]) {
            // globalObjectPointer = this; /// globalObjectMap
            var manage = '';
            manage += '<span class="hi-icon-effect-3 hi-icon-effect-3b" style="display:inline;">';

            //var isv = data[i]['IsVerified'];
            if (data[i]['IsVerified'] == 0 || data[i]['IsVerified'] == null) {
                if (this.isVerifiable[modelName]) manage += '<i id="v_' + data[i][col[0]] + '" style="margin-right: 4px;" class="hi-icon fa fa-check ' + modelName + ' verify-default-gray" data-vc="verify-default-gray"><a  href="#" onclick="setVerifyStatus(\'' + this.jgridBase + '\', this.parentNode,\'' + modelName + '\')"></a></i>';
            }
            else if (data[i]['IsVerified'] == 1) {
                if (this.isVerifiable[modelName]) manage += '<i id="v_' + data[i][col[0]] + '" style="margin-right: 4px;" class="hi-icon fa fa-check ' + modelName + ' verify-verified-green" data-vc="verify-verified-green"><a  href="#" onclick="setVerifyStatus(\'' + this.jgridBase + '\', this.parentNode,\'' + modelName + '\')"></a></i>';
            }
            else {
                if (this.isVerifiable[modelName]) manage += '<i id="v_' + data[i][col[0]] + '" style="margin-right: 4px;" class="hi-icon fa fa-check ' + modelName + ' verify-reject-red" data-vc="verify-reject-red"><a  href="#" onclick="setVerifyStatus(\'' + this.jgridBase + '\', this.parentNode,\'' + modelName + '\')"></a></i>';
            }

            if (this.isEditable[modelName]) manage += '<i style="margin-right: 4px;" class="hi-icon fa fa-pencil icon-fa "><a  href="#" onclick="onRecordEditProxy(' + this.jgridBase + ',' + data[i][col[0]] + ',\'' + modelName + '\', false)"></a></i>';
            if (this.isDeletable[modelName]) manage += '<i  class="hi-icon fa fa-trash icon-fa "><a  href="#" onclick="onRecordDeleteProxy(' + this.jgridBase + ',' + data[i][col[0]] + ',\'' + modelName + '\', false, this.onMasterItemClick)"></a></i>';
            manage += '</span>';
            tabCell.innerHTML = manage;
        }
    }
    if (this.isVerifiable[modelName]) this.addManageMenu(table, modelName);
}

function onVerifyDeselectAll(base, modelName) {
    $('.' + modelName).each(function (i, obj) {
        if ($(obj).hasClass('verify-selected-blue')) setVerifyStatus(base, obj, modelName);
    });
}

function onVerifySelectAll(base, modelName) {
    $('.' + modelName).each(function (i, obj) {
        if (!$(obj).hasClass('verify-selected-blue')) setVerifyStatus(base, obj, modelName);
    });
}

jGrid.prototype.addManageMenu = function (table, modelName) {
    //console.log(table);
    var popup =
        '<div class="dropdown">' +
        '<span>Manage </span><img class="dropbtn" style="width:10px;" src="../Images/arrow-down.png" />' +
        '<div class="dropdown-content">' +
            '<a onclick="onVerifySelectAll(\'' + this.jgridBase + '\',\'' + modelName + '\')" href="#">Select all Veriify</a>' +
            '<a onclick="onVerifyDeselectAll(\'' + this.jgridBase + '\',\'' + modelName + '\')" href="#">Deselect all Veriify</a>' +
        '</div>' +
        '</div>';

    $(table).find('thead>tr:eq(0)>th:eq(' + ($(table).find("thead>tr:eq(0)>th").length - 1) + ')').html(popup);
    $(table).find('thead>tr:eq(0)>td:eq(' + ($(table).find("thead>tr:eq(0)>td").length - 1) + ')').html(popup);
    // $(document).on('mouseenter', '.dropdown', function (event) { $('.dropdown-content').css('display', 'block'); });
}

jGrid.prototype.auto = function (modelName, data) {
    if (data.length == 0) return;

    if (this.columnWidth.length != this.columnName.length) {
        this.columnName = [];
        this.columnWidth = [];
    }
    for (var i = 0; i < this.columnWidth.length; i++) {
        this.columnWidth[i] = this.columnWidth[i] - (this.columnWidth[i] * this.manageItemsColWidth) / 100;
    }

    // EXTRACT VALUE FOR HTML HEADER. // ('Book ID', 'Book Name', 'Category' and 'Price')
    var col = [];
    for (var i = 0; i < data.length; i++) {
        for (var key in data[i]) {
            if (col.indexOf(key) === -1) {
                if (!key.includes('Color')) col.push(key);
            }
        }
    }
    if (this.isVerifiable[modelName] || this.isEditable[modelName] || this.isDeletable[modelName]) col.push('Manage');

    // CREATE DYNAMIC TABLE.
    var table = document.createElement("table");
    $(table).attr('id', modelName);

    table.className = "table table-bordered table-striped table-hover";
    if (this.isFlexible[modelName]) $(table).css('white-space', 'nowrap');

    //var table = document.getElementById("tblDetails").getElementsByTagName('tbody')[0];
    //---Set column this.columnWidth
    if (this.columnWidth.length > 0) {
        this.columnName.push('IsVerified');
        this.columnName.push('Manage');
        this.columnWidth.push(this.manageItemsColWidth);
        var co = '';
        for (var i = 0; i < col.length; i++) {
            co += '<col width="' + this.columnWidth[i] + '%">';
        }
        table.innerHTML = co;
    }

    // table.addClass("table table-bordered table-striped");
    // CREATE HTML TABLE HEADER ROW USING THE EXTRACTED HEADERS ABOVE.

    var tr = table.insertRow(-1);                   // TABLE ROW.

    var header = table.createTHead();           // TABLE HEADER.
    var row = header.insertRow(0);
    for (var i = 0; i < col.length; i++) {
        //var th = document.createElement("th"); 

        //var cell = row.insertCell(-1);
        var cell = document.createElement("th"); // insertCell done with create element TH
        row.appendChild(cell);

        $(cell).css('font-weight', 'bold');

        if (i != 0) {
            if (this.columnName.length > 0) cell.innerHTML = this.columnName[i - 1];  //th.innerHTML = this.columnName[i - 1];
            else cell.innerHTML = col[i].toUpperCase() === col[i] ? col[i] : col[i].match(/[A-Z][a-z]+|[0-9]+/g).join(" "); // FIXED by SHOHID uppercase problem solve
        }

        if (this.columnWidth.length == 0) if (i == col.length - 1) $(cell).css('width', this.manageItemsColWidth + '%'); // Manage Column this.columnWidth

        if (i == 0) $(cell).css('display', 'none');
        //tr.appendChild(th);
        if (col[i] == 'IsVerified' || col[i] == 'TotalRecord')///=============Added by Belayet        
            $(cell).css('display', 'none');//=======Added By Belayet 
    }
    //var tbody = $(table).find('tbody');
    this.addDataToTable(data, table, tr, col, modelName);

    var tableBaseName = modelName + 'FixedBase';
    $('#' + modelName).attr('id', tableBaseName);

    // FINALLY ADD THE NEWLY CREATED TABLE WITH JSON DATA TO A CONTAINER.
    var divContainer = document.getElementById(tableBaseName);
    divContainer.innerHTML = "";
    divContainer.appendChild(table);

    if (this.isFlexible[modelName]) this.flex(modelName);

    this.setDetailOnClick(modelName);

    //$('td:nth-child(1),th:nth-child(1)').hide();
}

jGrid.prototype.flex = function (modelName) {
    $('#' + modelName + 'FixedBase').wrap('<div style="position:relative;"></div>');
    $('#' + modelName + 'FixedBase').addClass('table-responsive');

    var $table = $('#' + modelName + 'FixedBase').children(":first");
    var $fixedColumn = $table.clone().attr('id', modelName + 'Clone').insertAfter($table).addClass('fixed-column');

    $fixedColumn.find('th:not(:last-child),td:not(:last-child)').remove();

    for (var i = 1; i < $fixedColumn.find("thead>tr").length; i++) {
        $($fixedColumn.find("thead>tr")[i]).remove()
    }
    $fixedColumn.find("thead>tr").height($($table.find("thead>tr")[0].children[1]).height() + 8);

    $fixedColumn.find('tr').each(function (i, elem) {
        $(this.children[0]).height($table.find('tr:eq(' + i + ')').height() - 8);//$table.find('tr:eq(' + i + ')').height());
        $(this.children[0]).removeClass('td_manage');
    });
    var column = 'table .td_manage';
    $(column).html('<i style="width:60px;" class="hi-icon fa fa-check verify-white-dummy"></i>');
}

jGrid.prototype.callDetailAction = function (detailObject, parm, gridBase) {  //isEdit, isDelete, isCustomTableX --- detailObject

    /////////this.isEditable = isEdit; isDeletable = isDelete;
    //var parm = { "ModelName": detailObject, "RequestType": "Show", "RecordType": "Detail", "RecordId": data };
    ////var parm = { "ModelName": detailObject, "RequestType": "Show", "RecordType": "Detail", "PageSize": this.pagingSize, "PageNo": 1, "Data": JSON.stringify(this.filter) }; //Changed Data to Filter -Belayet
    //this.this.isEditable = false;
    //this.auto(detailObject, null);
    var singlePaging = this.isSinglePaging[detailObject];
    var op = this;
    appetizer.actionCall(this.actionURL, parm, "GET", function (data, result) {
        if (result) {
            var serverReturn = data.Data;
            //if (typeof isEdit !== 'undefined' && isEdit != null) this.isEditable = isEdit;
            //if (typeof isDelete !== 'undefined' && isDelete != null) isDeletable = isDelete;
            if (op.isCustomTableHtml[detailObject]) op.custom(detailObject, serverReturn.Record); else op.auto(detailObject, serverReturn.Record);

            var total = 0;
            if (serverReturn.Record.length > 0) total = serverReturn.Record[0].TotalRecord;
            if (singlePaging) {
                op.createPaging(op, false, 'light-pagination-single' + gridBase, total);
            }
        }
        else {
            appetizer.message.showError('#showErrorMsg', data.Data, 10000);
        }
    });
}

jGrid.prototype.bindSingle = function (detailObject, data, isVerify, isEdit, isDelete, isFlex, isPaging, columnWidths, columnNames) {
    //this.clearDetail();
    this.init(true);
    this.columnName = [];
    this.columnWidth = [];

    this.currentMasterRecordId = data;
    this.detailModelName = detailObject;
    this.masterModelName = detailObject;
    if (typeof isFlex === 'undefined' || isFlex == null) this.isFlexible[detailObject] = false; else this.isFlexible[detailObject] = isFlex;
    if (typeof isVerify === 'undefined' || isVerify == null) this.isVerifiable[detailObject] = false; else this.isVerifiable[detailObject] = isVerify;  ///?
    if (typeof isEdit === 'undefined' || isEdit == null) this.isEditable[detailObject] = false; else this.isEditable[detailObject] = isEdit;
    if (typeof isDelete === 'undefined' || isDelete == null) this.isDeletable[detailObject] = false; else this.isDeletable[detailObject] = isDelete;
    if (typeof isPaging === 'undefined' || isPaging == null) this.isSinglePaging[detailObject] = false; else this.isSinglePaging[detailObject] = isPaging;

    var isCustomTable = false;

    if (document.getElementById(detailObject).nodeName.toUpperCase().includes('DIV')) isCustomTable = false;
    if (document.getElementById(detailObject).nodeName.toUpperCase().includes('TABLE')) isCustomTable = true;
    this.isCustomTableHtml[detailObject] = isCustomTable;

   
    setBackground();
    this.setSlimScroll('');

    if (!this.isSinglePaging[detailObject]) {

        $('#' + this.jgridBase + '  #DetailsCaption').css('display', 'none');
        $('#' + this.jgridBase + ' #pagingBase').css('display', 'none');

        $('#' + this.jgridBase + ' .j-grid-detail-layout').css('border', '0');
        $('#' + this.jgridBase + ' #content').css('padding', '5px');
        $('#' + this.jgridBase + ' #content').css('padding-bottom', '0');

        $('#' + this.jgridBase + ' .slimScrollDiv:first').height('490px');
        $('#content' + this.jgridBase).height('490px');
    } else {
        $('.j-grid-detail-layout').css('border', '0');
        $('#content').css('padding', '0');
        $('#content').css('padding-bottom', '20px');
    }

    if (this.isVerifiable[detailObject] == false) {
        $('#' + this.jgridBase + '  .j-grid-single-verification-base').remove();
        $('#' + this.jgridBase + '  .pagination-holder ').css('width', '100%');
    }

    if (!isCustomTable) {
        if (typeof columnWidths !== 'undefined' && columnWidths != null) {
            if (typeof columnNames !== 'undefined' && columnNames != null) {
                this.columnName = columnNames;
                this.columnWidth = columnWidths;
            }
        }
    }

    var parm = { "ModelName": detailObject, "RequestType": "Show", "RecordType": "Detail", "RecordId": this.currentMasterRecordId, "PageSize": this.pagingSize, "PageNo": 1, "Data": JSON.stringify(this.filter) };
    this.callDetailAction(detailObject, parm, this.jgridBase);
}

function onRecordVerifyProxy(base, isVerified) { //may be some of this method code will go to onRecordVerify???

    if (!globalObjectPointer[base].isVerifyByMaster) {
        var rec = {};
        var modelNameArr = globalObjectPointer[base].verifyModels.trim().split(' ');
        var modelName = modelNameArr.filter(function (el) {
            return el != '';
        });
        for (var i = 0; i < modelName.length; i++) {
            var model = modelName[i];
            $('.' + model + '.verify-selected-blue').each(function (i, obj) {
                if (typeof rec[model] === 'undefined') rec[model] = '';
                rec[model] = rec[model] + obj.id + ',';
            });
        }

        for (var key in rec) {
            if (rec.hasOwnProperty(key)) { // check if the property/key is defined in the object itself, not in parent  //UNDI                
                var ids = rec[key].replace(/v_/g, '');
                if (ids.slice(-1) == ',') {
                    ids = ids.slice(0, -1);
                }
                // action call for verify(model, ides) + reload data ? //globalObjectPointer[base.id].onRecordVerify(key, rec[key]);??
                globalObjectPointer[base].onRecordVerify(key, ids, 'Details', isVerified, globalObjectPointer[base].onMasterItemClick);
            }
        }
    }
    else {
        var masterRecordId = globalObjectPointer[base].currentMasterRecordId;
        var masterModel = globalObjectPointer[base].masterModelName;
        globalObjectPointer[base].onRecordVerify(masterModel, masterRecordId, 'Master', isVerified, globalObjectPointer[base].onMasterItemClick);
    }

    //var rec = {};
    //var modelNameArr = globalObjectPointer[base].verifyModels.trim().split(' ');
    //var modelName = modelNameArr.filter(function (el) {
    //    return el != '';
    //});
    //for (var i = 0; i < modelName.length; i++) {
    //    var model = modelName[i];
    //    $('.' + model + '.verify-selected-blue').each(function (i, obj) {
    //        if (typeof rec[model] === 'undefined') rec[model] = '';
    //        rec[model] = rec[model] + obj.id + ' ';
    //    });
    //}

    //for (var key in rec) {
    //    if (rec.hasOwnProperty(key)) { // check if the property/key is defined in the object itself, not in parent  //UNDI
    //        console.log(key + ':', rec[key].replace(/v_/g, ''));
    //        // action call for verify(model, ides) + reload data ? //globalObjectPointer[base.id].onRecordVerify(key, rec[key]);??
    //    }
    //}

    ////globalObjectPointer[base.id].onRecordVerify(id);  /// globalObjectMap
}

jGrid.prototype.onRecordVerify = function (modelName, ids, recordType, isVerified, notifyGrid) {  //modify this, add modelName, id and other varification info -- because verification shifted to detail-- it shoud work for both master and detail
    var note = $('#VerificationNote').val();
    //var parm = { "ModelName": this.masterModelName, "RequestType": "Verify", "RecordType": "Master", "RecordId": this.currentMasterRecordId, "Data": JSON.stringify({ IsVerified: id, VerificationNote: note }) }; //??? -Belayet
    var parm = { "ModelName": this.getEntityModel(modelName), "RequestType": "Verify", "RecordType": recordType, "RecordId": ids, "Data": JSON.stringify({ IsVerified: isVerified, VerificationNote: note }) }; //??? -Belayet
    var op = this;
    appetizer.actionCall(this.actionURL, parm, "GET", function (data, result) {
        if (result) {
            appetizer.message.showInfo('#showErrorMsg', 'Records is verified and sent notification to user.', 2500);
            //notifyGrid(op.currentMasterRecordId);
            if (notifyGrid != null) notifyGrid(op.currentMasterRecordId);
            else {
                parm = { "ModelName": modelName, "RequestType": "Show", "RecordType": "Detail", "RecordId": op.currentMasterRecordId, "PageSize": op.pagingSize, "PageNo": 1, "Data": JSON.stringify(op.filter) };
                op.callDetailAction(modelName, parm, op.jgridBase);
            }
        }
        else {
            appetizer.message.showError('#showErrorMsg', data.Data, 10000); //showing green atert.. why not red? -> set wrong acrionURL
        }
    });

}


//jGrid.prototype.onRecordVerify = function (id) {  //modify this, add modelName, id and other varification info -- because verification shifted to detail-- it shoud work for both master and detail

//    var note = $('#VerificationNote').val();
//    var parm = { "ModelName": this.masterModelName, "RequestType": "Verify", "RecordType": "Master", "RecordId": this.currentMasterRecordId, "Data": JSON.stringify({ IsVerified: id, VerificationNote: note }) }; //??? -Belayet

//    var op = this;
//    appetizer.actionCall(this.actionURL, parm, "GET", function (data, result) {
//        if (result) {
//            appetizer.message.showInfo('#showErrorMsg', 'Record is verified and sent notification to user.', 2500);
//            op.updateMasterData(op.currentMasterRecordId, id, note, data.VerifierName, data.VerificationDate);
//        }
//        else {
//            appetizer.message.showError('#showErrorMsg', data.Data, 10000); //showing green atert.. why not red? -> set wrong acrionURL
//        }
//    });
//}

jGrid.prototype.showVerifyInfo = function (modelName, recordId) {//show need to modify
    //alert(modelName + ' - ' + recordId);
    // action call and set value to spans
    var parm = { "ModelName": this.getEntityModel(modelName), "RequestType": "VerifyInfo", "RecordType": "Detail", "RecordId": recordId };
    var op = this;
    appetizer.actionCall(this.action, parm, "GET", function (data, result) {
        if (result) {
            var serverReturn = data.Data;

            if (data.Data.UserName != '') {
                var name, names = data.Data.UserName.split(" ");
                names.length > 1 ? name = names[1] : name = names[0]; // may be we can check mohammad, lenth, title for better name ??
                $("#lblCreated").html('Created by <span>' + name + '<span> on ' + getDate(data.Data.DataCollectionDate) + '</span>');
            }
            else {
                $("#lblCreated").html('User id not set');
            }
            if (data.Data.VerifierName != '') {
                $("#lblVerified").html('Verified By <span>' + data.Data.VerifierName + '<span> on ' + getDate(data.Data.VerificationDate) + '</span>');
            }
            else {
                $("#lblVerified").html('Recored not verifired');
            }

            if (data.Data.IsVerified == 0 || data.Data.IsVerified == null) {
                $("#lblStatus").html('Pending');
            }
            else if (data.Data.IsVerified == 1) {
                $("#lblStatus").html('Approved');
            }
            else if (data.Data.IsVerified == 2) {
                $("#lblStatus").html('Invalid data');
            }
            else if (data.Data.IsVerified == 3) {
                $("#lblStatus").html('Modified by User');
            }
            else {
                $("#lblStatus").html('');
            }

            $('#VerificationNote').val(data.Data.VerificationNote);
        }
        else {
            appetizer.message.showError('#showErrorMsg', data.Data, 10000);
        }
    });
}

jGrid.prototype.setVerificationInfo = function () { //discard - no need 

    if (this.selctedMasterJsonData.UserName != '') {
        var name, names = this.selctedMasterJsonData.UserName.split(" ");
        names.length > 1 ? name = names[1] : name = names[0]; // may be we can check mohammad, lenth, title for better name ??
        $("#lblCreated").html('Created by <span>' + name + '<span> on ' + getDate(this.selctedMasterJsonData.DataCollectionDate) + '</span>');
    }
    else {
        $("#lblCreated").html('User id not set');
    }
    if (this.selctedMasterJsonData.VerifierName != '') {
        $("#lblVerified").html('Verified By <span>' + this.selctedMasterJsonData.VerifierName + '<span> on ' + getDate(this.selctedMasterJsonData.VerificationDate) + '</span>');
    }
    else {
        $("#lblVerified").html('Recored not verifired');
    }

    if (this.selctedMasterJsonData.IsVerified == 0 || this.selctedMasterJsonData.IsVerified == null) {
        $("#lblStatus").html('Pending');
    }
    else if (this.selctedMasterJsonData.IsVerified == 1) {
        $("#lblStatus").html('Approved');
    }
    else if (this.selctedMasterJsonData.IsVerified == 2) {
        $("#lblStatus").html('Invalid data');
    }
    else if (this.selctedMasterJsonData.IsVerified == 3) {
        $("#lblStatus").html('Modified by User');
    }
    else {
        $("#lblStatus").html('');
    }

    $('#VerificationNote').val(this.selctedMasterJsonData.VerificationNote);
    if (this.selctedMasterJsonData.Caption != '') {
        $("#DetailsCaption").html(this.selctedMasterJsonData.Caption);
    }
    else {
        $("#DetailsCaption").html('Caption not set');
    }

}

jGrid.prototype.setDetailOnClick = function (modelName) {
    var listModeJgrid = true;
    var op = this;
    if (!this.isVerifyByMaster || this.isInvokeList[modelName]) {
        $('#' + modelName + ' td').click(function (e) {
            if (e.ctrlKey) {
                if (op.isInvokeList[modelName]) {
                    var colNo = parseInt($(this).index());  // may need plus col padding - row header td 
                    var rowNo = parseInt($(this).parent().index()) + $('#' + modelName + ' thead tr').length;

                    var rowValue = $('#' + modelName).find("tr:eq(" + rowNo + ")").find("td:eq(1)").html();  // which column may need to specify- now fixed to 1
                    var colValue = $('#' + modelName + ' th').eq(colNo).text();
                    var colName = $('#' + modelName + ' th').eq(colNo).text(); //Added by Shohid
                    var rowName = $('#' + modelName + ' th').eq(1).text();      //Added by Shohid

                    var url = '';
                    if (listModeJgrid) {
                        url = "/jgrid/showlist?model=" + op.showListModel[modelName][0] + "&caption=" + op.showListModel[modelName][1] + "&row=" + rowValue + "&col=" + colValue;
                    }
                    else {
                        var data = JSON.stringify({ 'rowName': rowName, 'rowValue': rowValue, 'colName': colName, 'colValue': colValue });
                        url = "/JGrid/ManageGrid?ControllerName=&ModelName=Student&RequestType=ListView&RecordType=Detail&RecordId=&Data=" + data + "&PageSize=0&PageNo=0";                     //Added by Shohid
                    }
                    window.open(url, '_blank').focus();

                    //alert(modelName + ' - ' + rowValue + ' - ' + colValue);
                    //window.location.href = url;
                    // action call, exec query sql, bind model, open back to jGrid singleBind in a new View to a new Brawser Tab. 
                }
            }
            else {
                if (op.isVerifiable[modelName]) {
                    var rowNo = parseInt($(this).parent().index()) + $('#' + modelName + ' thead tr').length;
                    var recordId = $('#' + modelName).find("tr:eq(" + rowNo + ")").find("td:eq(0)").html();
                    op.showVerifyInfo(modelName, recordId);
                }
            }
        });
    }
}

jGrid.prototype.updateMasterData = function (currentMasterRecordId, status, note, verifierName, verificationDate) {  //discard - no need
    this.selctedMasterJsonData.IsVerified = status;
    this.selctedMasterJsonData.VerificationNote = note;
    this.selctedMasterJsonData.VerifierName = verifierName;
    this.selctedMasterJsonData.VerificationDate = verificationDate;
    $("#" + currentMasterRecordId).attr('data-json', encodeURIComponent(JSON.stringify(this.selctedMasterJsonData)));
    this.setVerificationInfo();
}

jGrid.prototype.setActiveRow = function () {
    $(".j-grid-master-active").removeClass("j-grid-master-active");
    $("#" + this.currentMasterRecordId)[0].children[0].classList.add("j-grid-master-active");
}

function onDataExportProxy(base, filter) {
    globalObjectPointer[base.id].onDataExport(filter);  /// globalObjectMap
}

jGrid.prototype.onDataExport = function (filter) {

    var parm = { "ModelName": this.currentModelName, "RequestType": "Export", "RecordType": "Detail", "Data": JSON.stringify(this.filter) };

    appetizer.actionCall(this.actionURL, parm, "GET", function (data, result) {
        if (result) {
            appetizer.message.showInfo('#showErrorMsg', 'Data exported successfully', 2500);
        }
        else {
            appetizer.ajaxLoder.hidePleaseWait();
            appetizer.message.showError('#showErrorMsg', data.Data, 10000);
        }
    });
}

function showMapProxy(base) {
    globalObjectPointer[base.id].showMap();  /// globalObjectMap
}

// this.currentModelName set problem 
jGrid.prototype.showMap = function () {

    var parm = { "ModelName": this.getEntityModel(this.currentModelName), "RequestType": "Map", "RecordType": "Detail", "Data": JSON.stringify(this.filter) };

    appetizer.actionCall(this.actionURL, parm, "GET", function (data, result) {
        if (result) {

            $emodal = $("#mapModal");

            var locations = [];
            for (var i = 0; i < data.Data.length; i++) {
                locations.push([data.Data[i].Location, data.Data[i].Latitude, data.Data[i].Longitude, data.Data[i].Id]);
            }
            var map = new google.maps.Map(document.getElementById('map-canvas'), {
                zoom: 14,
                center: new google.maps.LatLng(23.7390734, 90.3609762),///It should be parametar
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            var infowindow = new google.maps.InfoWindow();

            var marker, i;

            for (i = 0; i < locations.length; i++) {
                marker = new google.maps.Marker({
                    position: new google.maps.LatLng(locations[i][1], locations[i][2]),
                    map: map
                });

                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        infowindow.setContent(locations[i][0]);
                        infowindow.open(map, marker);
                    }
                })(marker, i));
            }

            $emodal.modal("show");
        }
        else {
            appetizer.message.showError('#showErrorMsg', data.Data, 10000);
        }
    });
}

function getDate(value) {

    if (value == '' || value == null) {
        return null;
    } else {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var dt = new Date(parseFloat(results[1]));
        return dt.getFullYear() +
            "-" +
            ("0" + (dt.getMonth() + 1)).slice(-2) +
            "-" +
            ("0" + dt.getDate()).slice(-2);
    }
}

////////------------- Up and Down arrow events----------////
$(document).keydown(function (e) {
    //if not single //scroll prob
    var currentDiv = $(".j-grid-master-active").parent();
    var nextDiv = currentDiv.next();
    var prevDiv = currentDiv.prev();
    switch (e.which) {
        case 37: // left
            if (this.masterCurrentPage > 0) container.pagination('go', this.masterCurrentPage - 1);
            break;
        case 38: // up
            prevDiv.trigger("click");
            break;

        case 39: // right
            if (this.masterCurrentPage > 0) container.pagination('go', this.masterCurrentPage + 1);
            break;
        case 40: // down
            nextDiv.trigger("click");
            break;
            //case 17: // ctrl

            //    alert()
            //    break;
        default: return;
    }
    e.preventDefault();
});
//------ End -----------//

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};
function getModel() { return getUrlParameter('model'); }
function getRowVal() { return getUrlParameter('row'); }
function getColVal() { return getUrlParameter('col'); }
function getCaption() { return getUrlParameter('caption'); }


//all variables to proto --DONE
//updateMasterData -- DONE // not checked - check it
//setActiveRow  -- DONE
// recheck WHOLE thing -- TOMORROW NEVER DIES
// VERIFY + STICKY RIGHT CONTROL PANEL
//globalObjectPointer to MAP and multi grid check -- ??
// keydown proto  -- ??



//var this.jgridBase;
//var this.gridCaption = '';
//var this.actionURL = '';

// Set paging
//var container;
//function createPagingEx(totalRecord) {
//    container = $('#light-pagination').pagination({
//        dataSource: function (done) {
//            var result = [];
//            for (var i = 1; i < totalRecord; i++) { result.push(i); }
//            done(result);
//        },
//        pageSize: this.pagingSize,
//        showPageNumbers: false,
//        showNavigator: true,
//        showGoInput: true,
//        showGoButton: true,
//        triggerPagingOnInit: false,
//        callback: function (data, pagination) {
//            $isFirstCall = true;
//            if (pagination.direction != 0) {
//                var parm = { "ModelName": this.masterModelName, "RequestType": "Show", "RecordType": "Master", "PageSize": this.pagingSize, "PageNo": pagination.pageNumber, "Data": JSON.stringify(this.filter) };
//                this.callMasterAction(parm, this.masterModelName, this.masterTemplate, this.onMasterItemClick);
//            }
//            this.masterCurrentPage = pagination.pageNumber;
//        }
//    })
//    container.pagination('go', this.masterCurrentPage);
//    $("#TotalRecord").text(totalRecord);
//}


//--deselect all
// $('.dropdown-content').css('display', 'none');
//$('.' + modelName).each(function (i, obj) {
//    $(obj).addClass($(obj).attr('data-vc')).removeClass('verify-selected-blue');
//});

//--select all
// $('.dropdown-content').css('display', 'none');
//$('.' + modelName).each(function (i, obj) {
//    var className = $(obj).attr('class').split(' ');
//    className = className[className.length - 1];
//    if (className != 'verify-selected-blue') $(obj).attr('data-vc', className);
//    $(obj).addClass('verify-selected-blue').removeClass(className);
//});