import axios from 'axios';

export default {

//return all pets for user

petList(userId){
    return axios.get('/pets/:userId')

}

//return specific pet detail

//register new pet

//edit pet detail

//delete pet

}