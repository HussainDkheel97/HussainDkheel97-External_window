//import axios from 'axios';
import api from './api';

export default {

    NumbersOfReports(id) {
        return api.get(`/api/DashBords/GetTotal?ManagementId=${id}`);
        
     //   return axios.get(`http://mail:82/api/DashBords/GetTotal?ManagementId=${id}`);
    },

    
    NumbersOfReports2(id) {
        return api.get(`/api/DashBords/GetTotalSection?ManagementId=${id}`);
        
     //   return axios.get(`http://mail:82/api/DashBords/GetTotal?ManagementId=${id}`);
    },


        GettDashbordReport(id,from,To) {
        return api.get(`/api/DashBords/GettDashbordReport?ManagementId=${id}&&from=${from}&&To=${To}`);
        
     //   return axios.get(`http://mail:82/api/DashBords/GetTotal?ManagementId=${id}`);
    },

    
    
}