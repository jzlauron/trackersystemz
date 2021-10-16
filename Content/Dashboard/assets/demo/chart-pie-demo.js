// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

// Pie Chart Example
var ctx = document.getElementById("myPieChart");
var myPieChart = new Chart(ctx, {
  type: 'pie',
  data: {
      labels: ["Critical", "Open", "In Progress", "Closed" ],
    datasets: [{
        data: [12.21, 8.32, 11.25, 15.58 ],
        backgroundColor: ['#dc3545', '#28a745', '#ffc107' , '#000000'],
    }],
  },
});
