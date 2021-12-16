import axios from 'axios';

export default {


    //return user profile info

  displayUser(userId) {
    return axios.get(`/user/${userId}`)
  },

  displayUserProfile(){
    return axios.get('/myprofile')
  },
  // logged in user edit user profile

  editUser(userToUpdate){
    return axios.put(`/myprofile`, userToUpdate)
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

  getAllCities(){
    return axios.get(`/user/cities`)
  }

}