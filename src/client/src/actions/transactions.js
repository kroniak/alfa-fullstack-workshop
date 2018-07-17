import * as action from "./types";

import axios from "axios";

const ROOT_URL = "/api";

/**
 * Вытаскивает транзакции по картам пользователя
 *
 * @returns
 */
export const fetchTransactions = (number, skip = 0) => {
  if (skip < 0) skip = 0;

  return async dispatch => {
    try {
      dispatch({
        type: action.TRANS_FETCH_STARTED
      });

      const response = await axios.get(
        `${ROOT_URL}/transactions/${number}?skip=${skip}`
      );

      dispatch({
        type: action.TRANS_FETCH_SUCCESS,
        payload: { data: response.data, skip }
      });
    } catch (err) {
      dispatch({
        type: action.TRANS_FETCH_FAILED,
        payload: {
          error: err.response.data.message
            ? err.response.data.message
            : err.response.data,
          skip
        }
      });
      console.log(
        err.response.data.message
          ? err.response.data.message
          : err.response.data
      );
    }
  };
};
