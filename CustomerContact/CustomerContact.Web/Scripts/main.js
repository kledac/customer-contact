$(function () {
                //$('.datetimepicker').datetimepicker({
                //    format: 'MM/DD/YYYY'
                //});

            
                $(".datetimepicker").datetimepicker({
                    format: 'MM/DD/YYYY'
                }).on("dp.hide", function (e) {
                    var input = $(this).find("input:first");
                    input.trigger('input'); // Use for Chrome/Firefox/Edge
                    input.trigger('change'); });
            });

(function () {
    var app = angular.module("contactCustomer", ['angularUtils.directives.dirPagination']).value("Customers",data).value("UrlSite",urlSite);


    app.controller('TableController', function ($scope, $http, Customers, UrlSite) {

        $scope.allItems = Customers;
        $scope.DefaultCity = [{ "Id": 0, "Name": "All", "RegionId": 0 }];
        $scope.Cities = $scope.DefaultCity;
        $scope.UrlJson = UrlSite;


        $scope.totalItems = $scope.allItems.length;
         $scope.currentPage = 1;
         $scope.numPerPage = 2;
         $scope.searchName = '';
         $scope.list = Customers;

         $scope.sortType = 'Classification';
         $scope.sortDescending = false;  //

         $scope.sort = function(keyname){
            $scope.sortType = keyname;
            $scope.sortDescending = !$scope.sortDescending;
        }

        $scope.classFilter='0';
        $scope.genderF='0';
        $scope.nameFilter='';
        $scope.regionFilter='0';
        $scope.cityFilter = $scope.Cities[0];
        $scope.startDateFilter='';
        $scope.endDateFilter='';
        $scope.sellerFilter='0';

        $scope.clearFilters = function(){
            $scope.Cities = $scope.DefaultCity;
          $scope.classFilter='0';
          $scope.genderF='0';
          $scope.nameFilter='';
          $scope.regionFilter='0';
          $scope.cityFilter = $scope.Cities[0];
          $scope.startDateFilter='';
          $scope.endDateFilter='';
          $scope.sellerFilter='0';
          //$scope.sortType = 'Classification';
          //$scope.sortDescending = false;  //
        }

        $scope.filterByStartDate = function (item) {
            
            if ($scope.startDateFilter !== '') {
                var start = Date.parse($scope.startDateFilter);
                var itemDate = Date.parse(item.LastPurchase);
                if (isNaN(start) == false && isNaN(itemDate) == false) {
                    
                    var startDate = new Date(start);
                    var registerDate = new Date(itemDate);
                    
                    if (registerDate >= startDate)
                        return true;
                    else
                        return false;
                    
                } else
                    return true;

            } else
            {
                return true;
            }
        }

        $scope.filterByEndDate = function (item) {

            if ($scope.endDateFilter !== '') {
                var start = Date.parse($scope.endDateFilter);
                var itemDate = Date.parse(item.LastPurchase);
                if (isNaN(start) == false && isNaN(itemDate) == false) {

                    var startDate = new Date(start);
                    var registerDate = new Date(itemDate);

                    if (registerDate <= startDate)
                        return true;
                    else
                        return false;

                } else
                    return true;

            } else {
                return true;
            }
        }




        $scope.classificationFilter = function (item) {
            if ($scope.classFilter !== '0') {
                return compareInt($scope.classFilter, item.ClassificationId);
            }
          else
            return true;
        }
        $scope.regionChange = function () {

            if ($scope.regionFilter !== '0')
            {
                var dataObj = {
                    regionId: toInt($scope.regionFilter)
                };
                var res = $http.post($scope.UrlJson+'customer/GetCity', dataObj).then(function (response) {
                    
                    $scope.Cities = response.data;
                    $scope.cityFilter = $scope.Cities[0];
                });
            } else
            {
                $scope.Cities = $scope.DefaultCity;
                $scope.cityFilter = $scope.Cities[0];
            }
            
        }
        $scope.filterByRegion = function (item) {
            if ($scope.regionFilter !== '0') {
                return compareInt($scope.regionFilter, item.RegionId);
            }
            else {
                return true;
            }
        }

        $scope.filterSeller = function (item) {
           
            if ($scope.sellerFilter !== '0') {
                return compareInt($scope.sellerFilter, item.UserId);
            }
            else
                return true;
        }

        $scope.filterByCity = function (item) {
            
          if($scope.cityFilter.Id!==0){
            return compareInt($scope.cityFilter.Id,item.CityId);
          }
          else
            return true;
        }

        $scope.genderFilter = function (item) {
          if($scope.genderF!=='0'){
            return compareInt($scope.genderF,item.GenderId);
          }
          else
            return true;
        }

        $scope.filterByName = function(item){
          if($scope.nameFilter!==''){
            var filter = $scope.nameFilter.toLowerCase();
            var itemF = item.Name.toLowerCase();
            return itemF.indexOf(filter) !==-1;
          }else {

            return true;
          }
        }

        function toInt(a){
          return parseInt(a);
        }

        function compareInt(a,b){
          var filter = toInt(a);
          var itemF = toInt(b);

          return filter===itemF;
        }

        $scope.paginate = function(value) {
          var begin, end, index;
          begin = ($scope.currentPage - 1) * $scope.numPerPage;
          end = begin + $scope.numPerPage;
          index = $scope.allItems.indexOf(value);
          return (begin <= index && index < end);
        };





        $scope.resetAll = function () {
                $scope.filteredList = $scope.allItems;
                $scope.searchText = '';
                $scope.currentPage = 0;
                $scope.Header = ['', '', ''];
            }

    });
})();
