import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import NavNemu from './components/Layout';
import Points from './components/Points';
import Orders from './components/Orders';

import './App.css';

function App() {
    return (
        <div className="App">
            <NavNemu>
                <Router>
                    <Routes>
                        <Route path="/points" element={<Points />} />
                        <Route path="/orders" element={<Orders />} />
                    </Routes>
                </Router>
            </NavNemu>
        </div>
    );
}

export default App;
