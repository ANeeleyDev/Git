import axios from 'axios';

export default {

    //get all playdates

    playdateList() {
        return axios.get(`/playdates`);
    },
        // return logged in users list of playdates
    
    getPlaydatesByUserId(userId){
        return axios.get(`/playdates/user/${userId}`)
    },

        // return specific playdate details by playdate ID

    getPlaydateByPlaydateId(playdateId){
        return axios.get(`/playdates/${playdateId}`)
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
        return axios.delete(`/myplaydates/${playdateId}`)
    },

}