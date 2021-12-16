import axios from 'axios';

export default {


    //ANONYMOUS USER FUNCTIONS

    

   
        // return playdates from specific user
    
    getPlaydatesByUserId(userId){
        return axios.get(`/playdates/user/${userId}`)
    },

    getPlaydateForDisplay(playdateId){
        return axios.get(`/playdates/${playdateId}/display`)
    },
        //get all playdates
        
    getAllPlaydatesForDisplay(){
        return axios.get(`/playdates/display`)
    },

        // return any playdate by ID

    getPlaydateByPlaydateId(playdateId){
        return axios.get(`/playdates/${playdateId}`)
    },

        //LOGGED IN USER FUNCTIONS
        
        // return list of logged in users playdates

    getLoggedInUserPlaydates(){
        return axios.get(`/playdates/myplaydates/display`)
    },

        //creates new playdate
    registerPlaydate(playdateToSave){
        return axios.post(`/playdates/myplaydates/register`, playdateToSave)
    },

        //edit specific playdate

    updateLoggedInUserPlaydate(updatedPlaydate, playdateId){
        return axios.put(`/playdates/myplaydates/${playdateId}`,updatedPlaydate)
    },

        //delete playdate
    deleteLoggedInUserPlaydate(playdateId){
        return axios.delete(`playdates/myplaydates/${playdateId}`)
    },

    requestPlaydate(updatedPlaydate, playdateId){
        return axios.put(`/playdates/${playdateId}/request`, updatedPlaydate)
    },

    acceptPlaydate(updatedPlaydate, playdateId){
        return axios.put(`/playdates/${playdateId}/accept`, updatedPlaydate)
    },
    
    rejectPlaydate(updatedPlaydate, playdateId){
        return axios.put(`/playdates/${playdateId}/reject`, updatedPlaydate)
    },

    // ADMIN functions

    deletePlaydate(playdateId){
        return axios.delete(`/playdates/admin/${playdateId}`)
    },

    updatePlaydate(playdateId){
        return axios.put(`/playdates/admin/${playdateId}`)
    },

}