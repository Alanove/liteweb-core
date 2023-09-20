import { useState } from 'react'

import './App.css'
import Message from './Message'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <div><Message /></div>
    </>
  )
}

export default App
