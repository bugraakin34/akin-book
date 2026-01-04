import { Route, Routes } from 'react-router-dom'

function App() {
  return(
    <Routes>
      <Route path='/' element={<div>Home (Book list)</div>}/>
      <Route path='/login' element={<div>Login</div>}/>
      <Route path='/register' element={<div>Register</div>}/>
      <Route path='/books/:id' element={<div>Book detail</div>}/>
      <Route path='/favorites' element={<div>Favorites</div>}/>
      <Route path='/admin/books' element={<div>Admin Books</div>}/>
    </Routes>
  )
}

export default App
