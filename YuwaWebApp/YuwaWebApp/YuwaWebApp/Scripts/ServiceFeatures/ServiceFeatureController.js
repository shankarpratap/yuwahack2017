var app = angular.module('serviceFeatureApp', ['datatables']);

app.controller('serviceFeatureCtrl', function ($scope, $http) {
    $scope.Disabled = "Disabled";
    $scope.Enabled = "Enabled";
    $scope.PartiallyEnabled = "Partially Enabled";
    $scope.featureInfo = [];
    $scope.loadingFeatureInfo = true;

    $scope.initFeatureInfo = function() {
        $http.get("/api/servicefeaturesapi").then(function(response) {
            $scope.featureInfo = response.data;
            $scope.loadingFeatureInfo = false;
        });
    }

    $scope.isPartial = function(featureDetail) {
        return featureDetail != null && featureDetail.Value &&
            featureDetail.ExplicitMatches != null && featureDetail.ExplicitMatches.length > 0;
    }

    $scope.isEnabled = function(featureDetail) {
        return featureDetail != null && featureDetail.Value &&
            featureDetail.ExplicitMatches != null && featureDetail.ExplicitMatches.length === 0;
    }

    $scope.isDisabled = function(featureDetail) {
        return featureDetail != null && !featureDetail.Value;
    }

    $scope.getStatus = function (featureDetail) {
        if (featureDetail == null) {
            return "N/A";
        }

        if ($scope.isDisabled(featureDetail)) {
            return $scope.Disabled;
        } else {
            if (featureDetail.ExplicitMatches.length > 0) {
                return $scope.PartiallyEnabled;
            }
            else {
                return $scope.Enabled;
            }
        }
    }

    $scope.getExplicitMatches = function(featureDetail) {
        if (featureDetail == null || !featureDetail.Value) {
            return [];
        } else {
            return featureDetail.ExplicitMatches;
        }
    }

    $scope.getEnvironmentList = function() {
        return ["PPE", "PROD", "MoonCake", "BlackForest"];
    }
});