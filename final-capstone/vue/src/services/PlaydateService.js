import axios from 'axios';

export default {


    //ANONYMOUS USER FUNCTIONS

    //get all playdates

    playdateList() {
        return axios.get(`/playdates`);
    },
        // return playdates from specific user
    
    getPlaydatesByUserId(userId){
        return axios.get(`/playdates/user/${userId}`)
    },

        // return any playdate by ID

    getPlaydateByPlaydateId(playdateId){
        return axios.get(`/playdates/${playdateId}`)
    },

        //LOGGED IN USER FUNCTIONS
        
        // return list of logged in users playdates

    getLoggedInUserPlaydates(){
        return axios.get(`/playdates/myplaydates`)
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

    // ADMIN functions

    deletePlaydate(playdateId){
        return axios.delete(`/playdates/admin/${playdateId}`)
    },

    updatePlaydates(playdateId){
        return axios.put(`/playdates/admin/${playdateId}`)
    },

}