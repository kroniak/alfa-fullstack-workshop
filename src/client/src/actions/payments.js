import * as action from "./types";
import { fetchCard } from "./cards";
import { fetchTransactions } from "./transactions";

import axios from "axios";

const ROOT_URL = "/api";

/**
 * Проводит withdraw транзакцию
 *
 * @param {String} from
 * @param {String} to
 * @param {Integer} sum
 * @returns
 */
export const TransferMoney = (from, to, sum) => {
  // формируем транзакцию
  const transaction = {
    from,
    to,
    sum
  };

  return async dispatch => {
    try {
      const response = await axios.post(
        `${ROOT_URL}/transactions`,
        transaction
      );

      if (response.status === 201) {
        dispatch({
          type: action.PAYMENT_SUCCESS,
          payload: response.data
        });
        dispatch(fetchCard(from));
        dispatch(fetchTransactions(from));
      }
    } catch (err) {
      dispatch({
        type: action.PAYMENT_FAILED,
        payload: {
          error: err.response.data.message
            ? err.response.data.message
            : err.response.data,
          transaction
        }
      });

      dispatch(fetchTransactions(from));

      console.log(
        err.response.data.message
          ? err.response.data.message
          : err.response.data
      );
    }
  };
};

export const repeateTransferMoney = () => dispatch =>
  dispatch({
    type: action.PAYMENT_REPEAT
  });
