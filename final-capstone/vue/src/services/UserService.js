import axios from 'axios';

export default {


    //return user profile info

  displayUser(userId) {
    return axios.get(`/user/${userId}`)
  },
  // logged in user edit user profile

  editUser(userId){
    return axios.put(`/user/edit/${userId}`)
  },

  // logged in user delete profile 

  deleteUser(userId){
    return axios.delete(`/user/delete/${userId}`)
  },

  // ADMIN FUNCTION

  // update user role

  updateUserRole(updatedUser, userId){
    return axios.put(`/admin/${userId}`, updatedUser)
  },

}