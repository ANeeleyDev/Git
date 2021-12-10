import axios from 'axios';

export default {


    //return user profile info

  displayUser(userId) {
    return axios.get(`/user/${userId}`)
  },

  // dispaly logged in user profile

  displayLoggedInUser(){
    return axios.get(`/user/myprofile`)
  },
  // logged in user edit user profile

  updateLoggedInUser(){
    return axios.put(`/user/myprofile`)
  },

  // logged in user delete profile 

  deleteLoggedInUser(){
    return axios.delete(`/user/myprofile/`)
  },

  // ADMIN FUNCTION

  // update user role

  updateUserRole(updatedUser, userId){
    return axios.put(`/admin/${userId}/updaterole`, updatedUser)
  },

  deleteUser(userId){

    return axios.delete(`/admin/${userId}`)
  },

  updateUser(userId){
    return axios.put(`/admin/${userId}`)
  },

}